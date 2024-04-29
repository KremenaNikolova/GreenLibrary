import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import { List, Image, Pagination, Grid } from 'semantic-ui-react';
import './styles/categoryArticlesList.css'

export default function CategoryArticlesList() {
    const [articles, setArticles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [currentCategory, setCategory] = useState('');

    let { category } = useParams();

    const imageUrlBase = 'https://localhost:7195/Images/'

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/categories/${category}`, {
                    params: { page: currentPage }
                });
                setArticles(response.data);
                if (category != currentCategory) {
                    setCategory(category);
                    setCurrentPage(1);
                }
                const paginationHeader = response.headers['pagination'];
                if (paginationHeader) {
                    const paginationData = JSON.parse(paginationHeader);
                    setTotalPages(paginationData.TotalPageCount || 1);
                }
            } catch (error) {
                console.log(error);
            }
        };
        fetchArticles();
    }, [category, currentPage, currentCategory]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    return (
        <>
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
        </>
    );
}