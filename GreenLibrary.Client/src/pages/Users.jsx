import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Table, Button, Container, Grid, Pagination } from 'semantic-ui-react';
import DeactiveUserAdminModal from '../components/DeactiveUserAdminModal';
import axios from 'axios';
import "./styles/users.css";

export default function Users() {
    const [users, setUsers] = useState([]);

    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/admin/allusers`, {
                    params: { page: currentPage, pageSize: 10 }
                });

                setUsers(response.data);

                const paginationHeader = response.headers['pagination'];
                if (paginationHeader) {
                    const paginationData = JSON.parse(paginationHeader);
                    setTotalPages(paginationData.TotalPageCount || 1);
                }
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, [currentPage]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const handleUserDeactivation = (userId) => {
        setUsers(prevUsers => prevUsers.map(user =>
            user.id === userId ? { ...user, isDeleted: true } : user
        ));
    };

    return (
        <>
            <Container className="admin-panel-container">
                <div className="users-title-container">
                    <h1>Потребители</h1>
                </div>
                <Table celled>
                    <Table.Body>
                        {users.map(user => (
                            <Table.Row key={user.id}>
                                <Table.Cell className="username-cell"><Link to={`/user/${user.id}`}>{user.firstName} {user.lastName}</Link></Table.Cell>
                                <Table.Cell className="equal-width-cell"><Button className="approved">Изпращане на нова парола</Button></Table.Cell>
                                {user.isDeleted === false
                                    ? <Table.Cell className="equal-width-cell">
                                        <Button className="approved" disabled>Възстановяване на потребител</Button>
                                    </Table.Cell>
                                    :
                                    <Table.Cell className="equal-width-cell">
                                        <Button className="approved">Възстановяване на потребител</Button>
                                    </Table.Cell>}
                                {user.isModerator === true
                                    ?
                                    <Table.Cell className="equal-width-cell">
                                        <Button className="demote-moderator-btn">Премахни Модератор</Button>
                                    </Table.Cell>
                                    :
                                    <Table.Cell className="equal-width-cell">
                                        <Button className="promote-moderator-btn">Бъди Модератор</Button>
                                    </Table.Cell>
                                }
                                {user.isDeleted === false
                                    ?
                                    < Table.Cell className="equal-width-cell">
                                        <DeactiveUserAdminModal userDetails={user} onDeactivate={handleUserDeactivation} />
                                    </Table.Cell>
                                    :
                                    < Table.Cell className="equal-width-cell">
                                        <Button className="delete" disabled>Деактивиране на профила</Button>
                                    </Table.Cell>}
                            </Table.Row>
                        ))}
                    </Table.Body>
                </Table>
            </Container>
            {totalPages > 1 &&
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
                </Grid>}

        </>
    );
}