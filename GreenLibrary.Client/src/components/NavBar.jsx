import React, { useState } from 'react';
import { Menu, Container, Button, Input, Icon } from 'semantic-ui-react';
import { Link, useNavigate } from 'react-router-dom';
import './styles/navBar.css'
import DropDownCategories from './DropDownCategories';
import { useAuth } from '../hooks/AuthContext'


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";
    
    const { user, logout } = useAuth();
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const handleSearch = (e) => {
        e.preventDefault();
        navigate(`/search?query=${search}`); 
        setSearch('');
    };

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item >
                    <img src="/plant.png" alt="logo" className='logo' />
                </Menu.Item>
                <Menu.Item>
                    <Link to='/'>
                        <p>{title}</p>
                    </Link>
                </Menu.Item>
                <DropDownCategories />
                {user && (
                <Menu.Item>
                    <Link to="/create">
                        <Button positive content={create} />
                    </Link>
                    </Menu.Item>
                ) }
            </Container>
            <Menu.Menu position='right'>
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
                    <Menu.Item
                        name='logout'
                        onClick={logout}
                    >
                        Logout
                    </Menu.Item>
                ) : (
                    <Menu.Item>
                        <Link to="/login">
                            <p>Login</p>
                        </Link>
                    </Menu.Item>
                )}
            </Menu.Menu>
        </Menu>
    );
}
