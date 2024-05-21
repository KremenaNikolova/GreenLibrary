import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { List, Pagination, Label, Grid, Container, GridColumn } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import './styles/userFollowing.css';

export default function UserFollowers() {
    const { user, logout } = useAuth();
    const [followers, setFollowers] = useState([]);
    const [following, setFollowing] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [isFollowed, setIsFollowed] = useState(false);

    useEffect(() => {
        if (!user) {
            logout();
        } else {
            const fetchFollowing = async () => {
                try {
                    const followings = await axios.get(`https://localhost:7195/api/user/following`);
                    const response = await axios.get(`https://localhost:7195/api/user/follower`, {
                        params: { page: currentPage }
                    });
                    setFollowers(response.data);
                    setFollowing(followings.data);

                    const paginationHeader = response.headers['pagination'];
                    if (paginationHeader) {
                        const paginationData = JSON.parse(paginationHeader);
                        setTotalPages(paginationData.TotalPageCount || 1);
                    }
                } catch (error) {
                    console.log('Error fetching followers users:', error);
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
            setFollowing(followers.filter(user => user.id !== userId));
        } catch (error) {
            console.log('Error unfollow user:', error);
        }
    };

    const handleFollow = async (userId) => {
        try {
            await axios.post(`https://localhost:7195/api/user/follow`, null, {
                params: { followUserId: userId }
            });
            setFollowing([...following, { id: userId }]);
        } catch (error) {
            console.log('Error following user:', error);
        }
    };

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const isFollowing = (userId) => {
        return following.some(followedUser => followedUser.id === userId);
    };

    const handleFollowUnfollow = async (userId) => {
        if (isFollowing(userId)) {
            await handleUnfollow(userId);
        } else {
            await handleFollow(userId);
        }
    };

    return (
        <GridColumn stretched width={13}>
            <Container className='profile-articles-container' >
                <List divided>
                    {followers.map(user => (
                        <List.Item key={user.id}>
                            <List.Content floated='left'>
                                <Label as={Link} to={`/user/${user.id}`} basic size='large' className='ellipsis-label'>{user.firstName}  {user.lastName}</Label>
                            </List.Content>
                            <List.Content floated='right'>
                                {isFollowing(user.id) ? (
                                    <Label as='a' onClick={() => handleFollowUnfollow(user.id)} className="star-label" pointing='left' color='orange'>
                                        Спри да следваш
                                    </Label>
                                ) : (
                                        <Label as='a' onClick={() => handleFollowUnfollow(user.id)} className="star-label" pointing='left' color='green'>
                                        Последвай
                                    </Label>
                                )}
                            </List.Content>
                        </List.Item>
                    ))}
                </List>
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
                </Grid>
            </Container>
        </GridColumn>
    );
}