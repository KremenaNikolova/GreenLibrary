import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Table, Button, Container, Grid, Pagination } from 'semantic-ui-react';
import ActivateUserAdminModal from '../components/ActivateUserAdminModal';
import DeactiveUserAdminModal from '../components/DeactiveUserAdminModal';
import SendNewPasswordModal from '../components/SendNewPasswordModal';
import SortUsers from '../components/SortUsers';
import ToggleModeratorModal from '../components/ToggleModeratorModal';
import { useAuth } from '../hooks/AuthContext'
import axios from 'axios';
import "./styles/users.css";

export default function Users() {
    const [users, setUsers] = useState([]);

    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const [sortBy, setSortBy] = useState('createon-newest');

    const { user: authUser } = useAuth();

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/admin/allusers`, {
                    params: { page: currentPage, pageSize: 10, sortBy }
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
    }, [currentPage, sortBy]);

    const handlePaginationChange = (e, { activePage }) => {
        setCurrentPage(activePage);
    };

    const handleUserDeactivation = (userId) => {
        setUsers(prevUsers => prevUsers.map(user =>
            user.id === userId ? { ...user, isDeleted: true } : user
        ));
    };

    const handleUserActivation = (userId) => {
        setUsers(prevUsers => prevUsers.map(user =>
            user.id === userId ? { ...user, isDeleted: false } : user
        ));
    }

    const handleToggleModerator = (userId) => {
        setUsers(prevUsers => prevUsers.map(user =>
            user.id === userId ? { ...user, isModerator: !user.isModerator } : user
        ));
    };

    const handleSortChange = (sortValue) => {
        setSortBy(sortValue);
        setCurrentPage(1);
    };


    return (
        <>
            <Container className="admin-panel-container">
                <div className="users-title-container">
                    <h1>Потребители</h1>
                </div>
                <SortUsers onSortChange={handleSortChange} />
                <Table celled>
                    <Table.Header>
                        <Table.Row className="table-header">
                            <Table.HeaderCell>Име и фамилия</Table.HeaderCell>
                            <Table.HeaderCell>Потребителско име</Table.HeaderCell>
                            <Table.HeaderCell>Създадена на</Table.HeaderCell>
                            <Table.HeaderCell></Table.HeaderCell>
                            <Table.HeaderCell></Table.HeaderCell>
                            <Table.HeaderCell></Table.HeaderCell>
                            <Table.HeaderCell></Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {users.map(user => (
                            <Table.Row key={user.id}>
                                <Table.Cell className="username-cell"><Link to={`/user/${user.id}`}>{user.firstName} {user.lastName}</Link></Table.Cell>
                                <Table.Cell ><Link to={`/user/${user.id}`}>{user.username}</Link></Table.Cell>
                                <Table.Cell className="equal-width-cell" >{user.createdOn}</Table.Cell>
                                <Table.Cell className="equal-width-cell">
                                    <SendNewPasswordModal userDetails={user} />
                                </Table.Cell>
                                {user.isDeleted === false
                                    ? <Table.Cell>
                                        <Button className="approved" disabled>Възстановяване на потребител</Button>
                                    </Table.Cell>
                                    :
                                    <Table.Cell>
                                        <ActivateUserAdminModal userDetails={user} onActivate={handleUserActivation} />
                                    </Table.Cell>}
                                {authUser.roles === 'Admin' && authUser.id === user.id
                                    ?
                                    <Table.Cell className="equal-width-cell">
                                        <Button className="demote-moderator-btn" disabled>Премахни Модератор</Button>
                                    </Table.Cell>
                                    : user.isModerator === true
                                        ?
                                        <Table.Cell className="equal-width-cell">
                                            <ToggleModeratorModal userDetails={user} buttonMesasge="Премахни Модератор" className="demote-moderator-btn" onToggle={handleToggleModerator} />
                                        </Table.Cell>
                                        :
                                        <Table.Cell className="equal-width-cell">
                                            <ToggleModeratorModal userDetails={user} buttonMesasge="Бъди Модератор" className="promote-moderator-btn" onToggle={handleToggleModerator} />
                                        </Table.Cell>
                                }
                                {authUser.roles === 'Admin' && authUser.id === user.id
                                    ? <Table.Cell>
                                        <Button className="delete" disabled>Деактивиране на профила</Button>
                                    </Table.Cell>
                                    : user.isDeleted === false
                                        ? <Table.Cell>
                                            <DeactiveUserAdminModal userDetails={user} onDeactivate={handleUserDeactivation} />
                                        </Table.Cell>
                                        : <Table.Cell>
                                            <Button className="delete" disabled>Деактивиране на профила</Button>
                                        </Table.Cell>}

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
    );
}