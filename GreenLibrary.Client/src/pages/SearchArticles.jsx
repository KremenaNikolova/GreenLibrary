import React, { useState, useEffect } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { List, Image, Pagination, Grid } from 'semantic-ui-react';
import axios from 'axios';
import './styles/searchArticles.css';

function useQuery() {
    return new URLSearchParams(useLocation().search);
}

export default function SearchArticles() {
    const query = useQuery().get('query');
    const [results, setResults] = useState([]);

    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const imageUrlBase = 'https://localhost:7195/Images/'

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/articles/search?query=${query}`, {
                    params: { page: currentPage }
                });
                setResults(response.data);

                const paginationHeader = response.headers['pagination'];
                if (paginationHeader) {
                    const paginationData = JSON.parse(paginationHeader);
                    setTotalPages(paginationData.TotalPageCount || 1);
                }
            } catch (error) {
                console.error('Error fetching search results:', error);
            }
        };
        if (query) {
            fetchData();
        }
    }, [query, currentPage]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    return (
        <>
            <div className="h2-container">
                <h1>Резултати от търсенето</h1>
            </div>
            <List divided relaxed>
                {results.map(article => (
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
