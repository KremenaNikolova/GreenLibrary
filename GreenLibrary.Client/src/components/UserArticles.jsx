import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from "axios";
import { useAuth } from '../hooks/AuthContext';
import { Button, Label, GridColumn, Container, List, Pagination, Grid } from 'semantic-ui-react';
import EditArticle from './EditArticle';
import DeleteArticleModal from './DeleteArticleModal';
import './styles/userArticles.css';

export default function UserArticles() {
    const { user, logout } = useAuth();
    const [articles, setArticles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [editingArticleId, setEditingArticleId] = useState(null);

    useEffect(() => {
        if (!user) {
            logout();
        } else {
            const fetchArticles = async () => {
                try {
                    const response = await axios.get(`https://localhost:7195/api/user/articles`, {
                        params: { page: currentPage }
                    });

                    setArticles(response.data);
                    const paginationHeader = response.headers['pagination'];

                    if (paginationHeader) {
                        const paginationData = JSON.parse(paginationHeader);
                        setTotalPages(paginationData.TotalPageCount || 1);
                    }
                } catch (error) {
                    console.log("Error fetching articles:", error);
                }
            };
            fetchArticles();
        }
    }, [user, logout, currentPage]);


    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const handleEditClick = (articleId) => {
        setEditingArticleId(articleId);
    };

    const handleCancelEdit = () => {
        setEditingArticleId(null);
    };

    const handleDeleteSuccess = (articleId) => {
        setArticles(prevArticles => prevArticles.filter(article => article.id !== articleId));
    };

    return (
        <>
            {editingArticleId ? (
                <EditArticle articleId={editingArticleId} onCancel={handleCancelEdit} />
            ) : (
                <GridColumn stretched width={13}>
                    <Container className='profile-articles-container' >
                        <List divided>
                            {articles.map(article => (
                                <List.Item key={article.id}>
                                    <List.Content floated='left'>
                                        <Label as={Link} to={`/articles/${article.id}`} basic size='large' className='ellipsis-label'>{article.title}</Label>
                                    </List.Content>
                                    <List.Content floated='right'>
                                        <Button color='blue' onClick={() => handleEditClick(article.id)}>Редактирай</Button>
                                        <DeleteArticleModal articleId={article.id} onDeleteSuccess={handleDeleteSuccess} />
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
            )}
        </>
    );
}