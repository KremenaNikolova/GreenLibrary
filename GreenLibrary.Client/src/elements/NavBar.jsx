import { Menu, Container, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import './navbar.css'


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="/assets/plant.png" alt="logo"/>
                    {title}
                </Menu.Item>
                <Menu.Item name='Категории' />
                <Menu.Item>
                    <Link to="/create">
                        <Button positive content={create} />
                    </Link>
                </Menu.Item>
            </Container>
        </Menu>
    )
}
