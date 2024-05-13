import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Menu, Form, Input, Button, Image, GridColumn, Grid, Message, Modal } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import axios from 'axios';
import DeactivateUserModal from '../components/DeactivateUserModal';
import './styles/userSettings.css';
import UserProfileSettings from '../components/UserProfileSettings';

export default function UserSettings() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();
    const [activeItem, setActiveItem] = useState('profile');

    useEffect(() => {

        if (!user) {
            logout();
            navigate('/login');
        }
    }, [user, navigate, logout]);

    const handleItemClick = (name) => setActiveItem(name);

    const renderContent = () => {
        switch (activeItem) {
            case 'profile':
                return <UserProfileSettings />;
            default:
                return <UserProfileSettings />;
        }
    };

    return (
        <Grid className='profile-container'>
            <GridColumn width={3}>
                <Menu vertical fluid className='profile-menu'>
                    <Menu.Item name='profile' content='Моят профил' onClick={() => handleItemClick('profile')} active={activeItem === 'profile'} />
                    <Menu.Item as={Link} to='#' name='articles' content='Моите статии' onClick={() => handleItemClick('articles')}
                        active={activeItem === 'articles'} />
                    <Menu.Item as={Link} to='#' name='following' content='Потребители, които следвате' onClick={() => handleItemClick('following')} active={activeItem === 'following'} />
                    <Menu.Item as={Link} to='#' name='followers' content='Последователи' onClick={() => handleItemClick('followers')} active={activeItem === 'followers'} />
                </Menu>
            </GridColumn>
            <GridColumn stretched width={9}>
                {renderContent()}
            </GridColumn>
        </Grid>
    );
}