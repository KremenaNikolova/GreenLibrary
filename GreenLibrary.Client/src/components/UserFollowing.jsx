import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { List, Pagination, Label, Grid, Container, GridColumn } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import './styles/userFollowing.css';

export default function UserFollowing() {
    const { user, logout } = useAuth();
    const [following, setFollowing] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [noFollowingsMessage, setNoFollowingsMessage] = useState('Все още не сте последвали никого.');

    useEffect(() => {
        if (!user) {
            logout();
        } else {
            const fetchFollowing = async () => {
                try {
                    const response = await axios.get(`https://localhost:7195/api/user/following`, {
                        params: { page: currentPage }
                    });
                    setFollowing(response.data);
                    console.log(response.data)

                    const paginationHeader = response.headers['pagination'];
                    if (paginationHeader) {
                        const paginationData = JSON.parse(paginationHeader);
                        setTotalPages(paginationData.TotalPageCount || 1);
                    }
                } catch (error) {
                    console.log('Error fetching following users:', error);
                }
            };
            fetchFollowing();
        }
    }, [user, logout, currentPage]);

    const handleUnfollow = async (userId) => {
        try {
            await axios.post(`https://localhost:7195/api/user/unfollow`, null, {
                params: { followUserId: userId }
            });
            setFollowing(following.filter(user => user.id !== userId));
        } catch (error) {
            console.log('Error unfollowing user:', error);
        }
    };

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    return (
        <GridColumn stretched width={13}>
            <Container className='profile-articles-container' >
                {following.length > 0 ?
                    <List divided>
                        {following.map(user => (
                            <List.Item key={user.id}>
                                <List.Content floated='left'>
                                    <Label as={Link} to={`/user/${user.id}`} basic size='large' className='ellipsis-label'>{user.firstName}  {user.lastName}</Label>
                                </List.Content>
                                <List.Content floated='right'>
                                    <Label as='a' onClick={() => handleUnfollow(user.id)} className="star-label" pointing='left' color='orange'>
                                        Спри да следваш
                                    </Label>
                                </List.Content>
                            </List.Item>
                        ))}
                    </List>
                    :
                    <div className='noFolloewrsLabelContainer'>
                        <Label className='noFollowersMessage'>{noFollowingsMessage}</Label>
                    </div>}
                {totalPages > 1 &&
                    <Grid>
                        <Grid.Row>
                            <Grid.Column textAlign="center">

                                <Pagination
                                    activePage={currentPage}
                                    onPageChange={handlePaginationChange}
                                    totalPages={totalPages}
                                />
                            </Grid.Column>
                        </Grid.Row>
                    </Grid>}
            </Container>
        </GridColumn>
    );
}
