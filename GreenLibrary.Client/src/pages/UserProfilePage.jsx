import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import { Grid, Image, Button, List, Pagination, Container, Icon, Label } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import './styles/userProfilePage.css';

const imageUrl = 'https://localhost:7195/Images/';
export default function UserProfilePage() {
    const { userId } = useParams();
    const { user, logout } = useAuth();
    const [userProfile, setUserProfile] = useState(null);
    const [articles, setArticles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [isFollowed, setIsFollowed] = useState(false);
    const [isSameUser, setIsSameUsser] = useState(false);

    useEffect(() => {
        const fetchUserProfile = async () => {
            try {
                const currUser = await axios.get(`https://localhost:7195/api/user`);
                const response = await axios.get(`https://localhost:7195/api/user/${userId}`);
                setUserProfile(response.data);
                const followers = response.data.followers;
                if (followers.length > 0) {
                    const isFollowedUser = followers.some(follower => follower.id === currUser.data.id);
                    if (isFollowedUser) {
                        setIsFollowed(true);
                    }
                }

                setIsSameUsser(false);
                if (currUser.data.id === userId) {
                    setIsSameUsser(true);
                }
            } catch (error) {
                console.log('Error fetching user profile:', error);
            }
        };

        const fetchArticles = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/user/${userId}/articles`, {
                    params: { page: currentPage }
                });
                setArticles(response.data);

                const paginationHeader = response.headers['pagination'];
                if (paginationHeader) {
                    const paginationData = JSON.parse(paginationHeader);
                    setTotalPages(paginationData.TotalPageCount || 1);
                }
            } catch (error) {
                console.log('Error fetching articles:', error);
            }
        };

        fetchUserProfile();
        fetchArticles();
    }, [userId, currentPage]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const handleFollow = async () => {
        try {
            await axios.post(`https://localhost:7195/api/user/follow`, null, {
                params: { followUserId: userId }
            });
            setIsFollowed(true);
        } catch (error) {
            console.log('Error following user:', error);
        }
    };

    const handleUnfollow = async () => {
        try {
            await axios.post(`https://localhost:7195/api/user/unfollow`, null, {
                params: { followUserId: userId }
            });
            setIsFollowed(false);
        } catch (error) {
            console.log('Error unfollowing user:', error);
        }
    };

    if (!userProfile) {
        return <div>Loading...</div>;
    }

    return (
        <Grid className="profile-container">
            <Grid.Row>
                <Grid.Column width={3}>
                    <Button color="orange" className="back-button" onClick={() => window.history.back()}>НАЗАД</Button>
                    <Image className="profile-avatar" src={imageUrl + userProfile.image} size='medium' />
                    <div className="user-info">
                        <List.Item className='info-item' content={`Потребител:${'\u00A0'} ${userProfile.username}`} />
                        <List.Item className='info-item' content={`Брой статии:${'\u00A0'} ${userProfile.articlesCount}`} />
                        <List.Item className='info-item' content={`Брой последователи:${'\u00A0'} ${userProfile.followersCount}`} />
                    </div>
                </Grid.Column>
                <Grid.Column width={9}>
                    {!isSameUser && (isFollowed === true ?
                        <Button color="orange" floated='right' className="follow-button" onClick={handleUnfollow}>Спри да следваш</Button>
                        :
                        <Button color="orange" floated='right' className="follow-button" onClick={handleFollow}>Последвай</Button>)
                    }
                    
                    <Container className="articles-container">
                        <List divided verticalAlign='middle'>
                            {articles.map(article => (
                                <List.Item key={article.id}>
                                    <List.Content as={Link} to={`/articles/${article.id}`} floated='left' className="article-title">
                                        {article.title}
                                    </List.Content>
                                    <List.Content floated='right'>
                                        <Label className="star-label" pointing='left'>
                                            <Icon name='star' size="large" className="user-profile-star-icon" /> {article.likes}
                                        </Label>
                                    </List.Content>
                                </List.Item>
                            ))}
                        </List>
                    </Container>
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
                </Grid.Column>
            </Grid.Row>
        </Grid>
    );
}
