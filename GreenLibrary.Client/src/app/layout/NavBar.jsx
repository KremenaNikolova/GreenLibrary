import { Menu, Container, Button } from 'semantic-ui-react';


export default function NavBar() {
    const title = "Зелена библиотека";
    const create = "Нова статия";
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="/assets/plant.png" alt="logo" style={{ marginRight: '10px'}} />
                    { title }
                </Menu.Item>
                <Menu.Item name='Категории'/>
                <Menu.Item>
                    <Button positive content={ create } /> 
                </Menu.Item>
            </Container>
        </Menu>
    )
}
