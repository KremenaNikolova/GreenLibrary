import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { List, Image, Pagination, Grid } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import SortArticles from '../components/SortArticles';
import axios from 'axios';
import './styles/categoryArticlesList.css'

export default function CategoryArticlesList() {
    const [articles, setArticles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const { user, logout } = useAuth();

    const [sortBy, setSortBy] = useState('createon-newest');

    const imageUrlBase = 'https://localhost:7195/Images/'

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/articles`, {
                    params: { page: currentPage, sortBy }
                });

                setArticles(response.data);
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
    }, [currentPage, sortBy]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const handleSortChange = (sortValue) => {
        setSortBy(sortValue);
        setCurrentPage(1);
    };

    return (
        <>
            <List divided relaxed>
            <SortArticles onSortChange={handleSortChange} /> 
                {articles.map(article => (
                    <List.Item key={article.id}>
                        {article.isApproved == true && (
                            <>
                                <Image src={imageUrlBase + article.image} size='tiny' floated='left' />
                                <List.Header as={Link} to={`/articles/${article.id}`} id="article-list-title">{article.title}</List.Header>
                                <Link to={user ? `/user/${article.userId}` : `/login` }>
                                    <List.Description className='article-author-link'>
                                        Автор: {article.user}
                                    </List.Description>
                                </Link>
                                <List.Description className='date'>
                                    Създадена на: {article.createdOn}
                                </List.Description>
                            </>
                        )}
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