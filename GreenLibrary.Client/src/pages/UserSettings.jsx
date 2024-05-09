import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Menu, Form, Input, Button, Image, GridColumn, Grid } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import axios from 'axios';
import './styles/userSettings.css';

export default function UserSettings() {
    const { user } = useAuth();
    const navigate = useNavigate();
    const [userDetails, setUserDetails] = useState();
    const [activeItem, setActiveItem] = useState('profile');
    const [errors400, setErrors400] = useState({});


    useEffect(() => {

        if (!user) {
            navigate('/login');
        }
        axios.get("https://localhost:7195/api/user").then((response) => {
            setUserDetails(response.data);
            console.log(response.data);
        });
    }, [user, navigate]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append('firstName', userDetails.firstName);
        formData.append('lastName', userDetails.lastName);
        formData.append('username', userDetails.username);
        formData.append('email', userDetails.email);

        if (userDetails.imageFile) {
            formData.append('imageFile', userDetails.imageFile);
        }

        if (userDetails.oldPassword && userDetails.newPassword) {
            formData.append('oldPassword', userDetails.oldPassword);
            formData.append('newPassword', userDetails.newPassword);
            formData.append('repeatNewPassword', userDetails.repeatNewPassword);
        }

        try {
            const response = await axios({
                method: 'put',
                url: 'https://localhost:7195/api/user',
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            console.log('User edited:', response.data);

        } catch (error) {
            setErrors400({});
            if (error.response && error.response.status === 400) {
                setErrors400(error.response.data.errors);
            } else {
                console.error('Edit failed:', error.response.data);
                console.error('Failed to edit the user:', error);
            }
        }

    };


    const handleItemClick = (name) => setActiveItem(name);

    return (
        <Grid className='profile-container'>
            <GridColumn width={3}>
                <Menu vertical fluid className='profile-menu'>
                    <Menu.Item as={Link} to='/user/settings' name='profile' content='Моят профил' onClick={() => handleItemClick('profile')} active={activeItem === 'profile'} />
                    <Menu.Item as={Link} to='#' name='articles' content='Моите статии' onClick={() => handleItemClick('articles')}
                        active={activeItem === 'articles'} />
                    <Menu.Item as={Link} to='#' name='following' content='Потребители, които следвате' onClick={() => handleItemClick('following')} active={activeItem === 'following'} />
                    <Menu.Item as={Link} to='#' name='followers' content='Последователи' onClick={() => handleItemClick('followers')} active={activeItem === 'followers'} />
                </Menu>
            </GridColumn>

            {userDetails &&
                <GridColumn stretched width={7}>
                    <Form onSubmit={handleSubmit} className='profile-details-container'>
                        <Form.Group widths='equal'>
                            <Form.Field
                                control={Input}
                                label='Име'
                                value={userDetails.firstName}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    firstName: e.target.value
                                }))}
                            />
                            <Form.Field
                                control={Input}
                                label='Фамилия'
                                value={userDetails.lastName}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    lastName: e.target.value
                                }))}
                            />
                        </Form.Group>

                        <Form.Group widths='equal'>
                            <Form.Field
                                control={Input}
                                label='Потребителско име'
                                value={userDetails.username}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    username: e.target.value
                                }))}
                            />
                            <Form.Field
                                control={Input}
                                label='Имейл'
                                value={userDetails.email}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    email: e.target.value
                                }))}
                            />
                        </Form.Group>

                        <Form.Group>
                            <Form.Field className='image-field'>
                                <Image src='../../public/plant.png' size='small' widths={2} className='profile-image' />
                            </Form.Field>
                            <Form.Field widths={2}>
                                <label>Промени снимката на профила</label>
                                <Input
                                    type="file"
                                    accept="image/*"
                                    onChange={(e) => setUserDetails(prevDetails => ({
                                        ...prevDetails,
                                        imageFile: e.target.files[0]
                                    }))}
                                />
                            </Form.Field>
                        </Form.Group>

                        <Form.Group widths={2}>
                            <Form.Field
                                control={Input}
                                label='Стара парола'
                                type='password'
                                placeholder='**********'
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    oldPassword: e.target.value
                                }))}
                            />
                        </Form.Group>

                        <Form.Group widths='equal'>
                            <Form.Field
                                control={Input}
                                type='password'
                                label='Нова парола'
                                placeholder='**********'
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    newPassword: e.target.value
                                }))}
                            />
                            <Form.Field
                                control={Input}
                                type='password'
                                label='Повтори новата парола'
                                placeholder='**********'
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    repeatNewPassword: e.target.value
                                }))}
                            />
                        </Form.Group>

                        <div className="sumbit button container">
                            <Button type='submit' className='deactivate-btn'>Деактивиране на профила</Button>
                            <Button type='submit' className='save-btn'>Запазване</Button>
                        </div>
                    </Form>
                </GridColumn>
            }
        </Grid>
    );
}