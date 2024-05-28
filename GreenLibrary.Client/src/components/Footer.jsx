import React from 'react';
import { Container, Grid, Header, List, Menu, Icon } from 'semantic-ui-react';
import './styles/footer.css';

export default function Footer() {
    return (
        <Menu inverted fixed='bottom' className='menu-footer'>
            <Container className='footer-container'>
                <Grid divided>
                    <Grid.Row>
                        <Grid.Column width={8}>
                            <Header inverted as='h2' content='Green Library' />
                            <p>
                                Green Library е основана с мисията да предоставя достъпна информация за земедлието и околната среда. Нашата цел е да помогнем на хората да обогатят своите знания в света на селското стопанство, както и да споделят своят личен опит.
                            </p>
                        </Grid.Column>
                        <Grid.Column width={4}>
                            <Header inverted as='h3' content='Контакти' />
                            <List link inverted>
                                <p>©2024 Green Library</p>
                                <p>Имейл: info@greenlibrary.com</p>
                                <p>Телефон: +359 123 456 789</p>
                                <p>Адрес: ул. Примерна 123, Нови пазар 9900, България</p>
                            </List>
                        </Grid.Column>
                        <Grid.Column width={4}>
                            <Header inverted as='h3' content='Последвайте ни' />
                            <List link inverted>
                                <List.Item as='a' href='#'>
                                    <Icon name='facebook' /> Facebook
                                </List.Item>
                                <List.Item as='a' href='#'>
                                    <Icon name='instagram' /> Instagram
                                </List.Item>
                                <List.Item as='a' href='#'>
                                    <Icon name='youtube' /> YouTube
                                </List.Item>
                            </List>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
            </Container>
        </Menu>
    );
}