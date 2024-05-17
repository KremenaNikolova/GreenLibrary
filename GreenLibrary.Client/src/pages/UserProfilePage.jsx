import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { Grid, Image, Button, List, Pagination, Container, Icon, Label } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import './styles/userProfilePage.css';

const imageUrl = 'https://localhost:7195/Images/'
export default function UserProfilePage() {
    const { userId } = useParams();
    const { user, logout } = useAuth();
    const [userProfile, setUserProfile] = useState(null);
    const [articles, setArticles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        const fetchUserProfile = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/user/${userId}`);
                setUserProfile(response.data);
                console.log("Profile data: ", response.data);
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
                console.log("Response data ", response.data);
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

    if (!userProfile) {
        return <div>Loading...</div>;
    }

    return (
        <Grid className="profile-container">
            <Grid.Row>
                <Grid.Column width={3}>
                    <Button color="orange" className="back-button" onClick={() => window.history.back()}>НАЗАД</Button>
                    <Image className="profile-avatar" src={imageUrl + userProfile.image || 'default-profile.png'} size='medium' />
                    <div className="user-info">
                        <List.Item className='info-item' fluid content={`Потребител:${'\u00A0'} ${userProfile.username}`} />
                        <List.Item className='info-item' fluid content={`Брой статии:${'\u00A0'} ${userProfile.articlesCount}`} />
                        <List.Item className='info-item' fluid content={`Брой последователи:${'\u00A0'} ${userProfile.followersCount}`} />
                    </div>
                </Grid.Column>
                <Grid.Column width={13}>
                    <Container className="articles-container">
                        <List divided>
                            {articles.map(article => (
                                <List.Item key={article.id}>
                                    <List.Content floated='left'>
                                        <Button as='div' labelPosition='right'>
                                            <Button basic fluid color='orange' content={article.title} />
                                            <Label as='a' basic color='orange' pointing='left'>
                                                <Icon name='star' /> {article.likesCount}
                                            </Label>
                                        </Button>
                                    </List.Content>
                                </List.Item>
                            ))}
                        </List>
                    </Container>
                    <Pagination
                        activePage={currentPage}
                        onPageChange={handlePaginationChange}
                        totalPages={totalPages}
                    />
                </Grid.Column>
            </Grid.Row>
        </Grid>
    );
}
