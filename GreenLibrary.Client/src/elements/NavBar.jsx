import { Menu, Container, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import './styles/navBar.css'
import DropDownCategories from './DropDownCategories';


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";

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
        </Menu>
    )
}
