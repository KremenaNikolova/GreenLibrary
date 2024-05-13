import React, { useState, useEffect } from 'react';
import axios from "axios";
import { useAuth } from '../hooks/AuthContext';
import { Button, Label, GridColumn, Container, List } from 'semantic-ui-react';
import './styles/userArticles.css';

export default function UserArticles() {
    const { user, logout } = useAuth();
    const [articles, setArticles] = useState([]);

    useEffect(() => {
        if (!user) {
            logout();
        } else {
            axios.get("https://localhost:7195/api/user/articles")
                .then((response) => {
                    setArticles(response.data);
                })
                .catch(error => {
                    console.log("Error fetching articles:", error);
                });
        }
    }, [user, logout]);

    return (
        <GridColumn stretched width={13}>
            <Container className='profile-articles-container' >
                <List divided>
                    {articles.map(article => (
                        <List.Item key={article.id}>
                            <List.Content floated='left'>
                                <Label basic size='large' className='ellipsis-label'>{article.title}</Label>
                            </List.Content>
                            <List.Content floated='right'>
                                <Button  color='blue'>Редактирай</Button>
                                <Button  color='red'>Изтрий</Button>
                            </List.Content>
                        </List.Item>
                    ))}
                </List>
            </Container>
        </GridColumn>
    );
}