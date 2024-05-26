import React, { useState } from 'react';
import { Menu, Container, Button, Input, Icon } from 'semantic-ui-react';
import { Link, useNavigate } from 'react-router-dom';
import DropDownCategories from './DropDownCategories';
import DropDownProfile from './DropDownProfile';
import { useAuth } from '../hooks/AuthContext'
import './styles/navBar.css'
import DropDownAdminMenu from './DropDownAdminMenu';


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";
    const allArticls = "Всички статии";

    const { user, logout } = useAuth();
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const handleSearch = (e) => {
        e.preventDefault();
        navigate(`/search?query=${search}`);
        setSearch('');
    };

    const handleHomeClick = () => {
        window.location.href = '/';
    }

    const handleCreateClick = () => {
        window.location.href = '/create';
    }

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item >
                    <img src="/plant.png" alt="logo" className='logo' />
                </Menu.Item>
                <Menu.Item onClick={handleHomeClick}>
                    <p>{title}</p>
                </Menu.Item>
                <DropDownCategories />
                <Menu.Item>
                    <Link to='/articles'>
                        <p>{allArticls}</p>
                    </Link>
                </Menu.Item>
                {user && (
                    <Menu.Item>
                        <Button positive content={create} onClick={handleCreateClick} />
                    </Menu.Item>
                )}
            </Container>
            <Menu.Menu position='right'>
                {user && (user.roles === 'Admin' || user.roles === 'Moderator') && (
                    <DropDownAdminMenu user={user} />
                )}
                <Menu.Item>
                    <Input
                        icon={<Icon name='search' link onClick={handleSearch} />}
                        placeholder='Search...'
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                        onKeyPress={(e) => {
                            if (e.key === 'Enter') {
                                handleSearch(e);
                            }
                        }}
                    />
                </Menu.Item>
                {user ? (
                    <DropDownProfile logout={logout} />
                ) : (
                    <Menu.Item>
                        <Link to="/login">
                            <p>Вход</p>
                        </Link>
                    </Menu.Item>
                )}
            </Menu.Menu>
        </Menu>
    );
}
