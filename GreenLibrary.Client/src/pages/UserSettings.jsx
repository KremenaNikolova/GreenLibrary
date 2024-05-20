import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Menu, GridColumn, Grid, Button } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import UserProfileSettings from '../components/UserProfileSettings';
import UserArticles from '../components/UserArticles';
import './styles/userSettings.css';

export default function UserSettings() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const [activeItem, setActiveItem] = useState(location.state?.activeItem || 'profile');

    useEffect(() => {

        if (!user) {
            logout();
            navigate('/login');
        }
    }, [user, navigate, logout]);

    useEffect(() => {
        if (location.state?.activeItem) {
            setActiveItem(location.state.activeItem);
        }
    }, [location.state]);

    const handleItemClick = (name) => setActiveItem(name);

    const renderContent = () => {
        switch (activeItem) {
            case 'profile':
                return <UserProfileSettings />;
            case 'articles':
                return <UserArticles />;
            default:
                return <UserProfileSettings />;
        }
    };

    return (
        <Grid className='profile-container'>
            <GridColumn width={3}>
                <Menu vertical fluid className='profile-menu'>
                    <Menu.Item name='profile' content='Моят профил' onClick={() => handleItemClick('profile')} active={activeItem === 'profile'} />
                    <Menu.Item name='articles' content='Моите статии' onClick={() => handleItemClick('articles')}
                        active={activeItem === 'articles'} />
                    <Menu.Item name='following' content='Потребители, които следвате' onClick={() => handleItemClick('following')} active={activeItem === 'following'} />
                    <Menu.Item name='followers' content='Последователи' onClick={() => handleItemClick('followers')} active={activeItem === 'followers'} />
                </Menu>
            </GridColumn>
            <GridColumn stretched width={9}>
                {renderContent()}
            </GridColumn>
        </Grid>
    );
}