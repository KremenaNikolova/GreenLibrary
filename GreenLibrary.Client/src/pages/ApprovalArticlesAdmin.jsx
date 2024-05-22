import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Table, Button, Container, Grid, Pagination } from 'semantic-ui-react';
import ApproveModal from '../components/ApproveModal';
import DeleteArticleModal from '../components/DeleteArticleModal';
import EditArticle from '../components/EditArticle';
import axios from 'axios';
import './styles/approvalArticlesAdmin.css';

export default function ApprovalArticlesAdmin() {
    const [articles, setArticles] = useState([]);
    const [editingArticleId, setEditingArticleId] = useState(null);

    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        const fetchArticles = async () => {
            try {
                const response = await axios.get('https://localhost:7195/api/admin/articles', {
                    params: { page: currentPage, pageSize: 10 }
                });
                setArticles(response.data);
                console.log(response.data)

                const paginationHeader = response.headers['pagination'];
                if (paginationHeader) {
                    const paginationData = JSON.parse(paginationHeader);
                    setTotalPages(paginationData.TotalPageCount || 1);
                }
            } catch (error) {
                console.error('Error fetching articles:', error);
            }
        };

        fetchArticles();
    }, [currentPage]);

    const handleEdit = (articleId) => {
        setEditingArticleId(articleId);
    };

    const handleCancelEdit = () => {
        setEditingArticleId(null);
    };

    const handleDelete = (articleId) => {
        setArticles(prevArticles => prevArticles.filter(article => article.id !== articleId));
    };

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    return (
        <>
            {editingArticleId ? (
                <EditArticle articleId={editingArticleId} onCancel={handleCancelEdit} />
            ) : (
                <>
                    <Container className="admin-panel-container">
                        <Table celled>
                            <Table.Header>
                                <Table.Row className="table-header">
                                    <Table.HeaderCell>Име на статията</Table.HeaderCell>
                                    <Table.HeaderCell>Автор</Table.HeaderCell>
                                    <Table.HeaderCell>Създадена на</Table.HeaderCell>
                                    <Table.HeaderCell>Одобряване</Table.HeaderCell>
                                    <Table.HeaderCell>Редактиране</Table.HeaderCell>
                                    <Table.HeaderCell>Изтриване</Table.HeaderCell>
                                </Table.Row>
                            </Table.Header>
                            <Table.Body>
                                {articles.map(article => (
                                    <Table.Row key={article.id}>
                                        <Table.Cell><Link to={`/articles/${article.id}`}>{article.title}</Link></Table.Cell>
                                        <Table.Cell><Link to={`/user/${article.userId}`}>{article.user}</Link></Table.Cell>
                                        <Table.Cell>{article.createdOn}</Table.Cell>
                                        {article.isApproved === true
                                            ?
                                            <Table.Cell>
                                                <Button className="approved" disabled>Одобрена</Button>
                                            </Table.Cell>
                                            :
                                            <Table.Cell>
                                                {/*<Button className="approved" onClick={() => handleApprove(article.id)}>Одобряване</Button>*/}
                                                <ApproveModal articleId={article.id} articles={articles} setArticles={setArticles} />
                                            </Table.Cell>
                                        }
                                        <Table.Cell>
                                            <Button className='edit' onClick={() => handleEdit(article.id)}>Редактиране</Button>
                                        </Table.Cell>
                                        <Table.Cell>
                                            {/*<Button className='delete' onClick={() => handleDelete(article.id)}>Изтриване</Button>*/}
                                            <DeleteArticleModal articleId={article.id} onDeleteSuccess={handleDelete} />
                                        </Table.Cell>
                                    </Table.Row>
                                ))}
                            </Table.Body>
                        </Table>
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
                </>
            )}
        </>
    );
}