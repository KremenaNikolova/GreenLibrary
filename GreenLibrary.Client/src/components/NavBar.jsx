import { Menu, Container, Button, Input } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import './styles/navBar.css'
import DropDownCategories from './DropDownCategories';
import { useAuth } from '../hooks/AuthContext'


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";

    const { user, logout } = useAuth();

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
                <Menu.Item>
                    <Link to="/create">
                        <Button positive content={create} />
                    </Link>
                </Menu.Item>
            </Container>
            <Menu.Menu position='right'>
                <Menu.Item>
                    <Input icon='search' placeholder='Search...' />
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
