import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import { List, Image } from 'semantic-ui-react';
import './styles/categoryArticlesList.css'

export default function CategoryArticlesList() {
    const [articles, setArticles] = useState([]);
    let { category } = useParams();
    const imageUrlBase = 'https://localhost:7195/Images/'

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/categories/${category}`);
                setArticles(response.data)
            } catch (error) {
                console.log(error);
            }
        };
        fetchArticles();
    }, [category]);

    return (
        <List divided relaxed>
            {articles.map(article => (
                <List.Item key={article.id}>
                    <Image src={imageUrlBase + article.image} size='tiny' floated='left' />
                    <List.Header as={Link} to={`/articles/${article.id}`} id="article-list-title">{article.title}</List.Header>
                    <List.Description >
                        {article.user} - {article.createdOn}
                    </List.Description>
                </List.Item>
            ))}
        </List>
    );
}