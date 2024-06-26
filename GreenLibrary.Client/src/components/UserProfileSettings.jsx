import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Input, Button, Image, GridColumn, Message } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import DeactivateUserModal from './DeactivateUserModal';
import axios from 'axios';

const imageUrl = 'https://localhost:7195/Images/';

export default function UserProfileSettings() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();
    const [userDetails, setUserDetails] = useState();
    const [errors400, setErrors400] = useState({});
    const [errorString, setErrorString] = useState("");


    useEffect(() => {

        if (!user) {
            logout();
            navigate('/login');
        }
        axios.get("https://localhost:7195/api/user").then((response) => {
            setUserDetails(response.data);
        });
    }, [user, navigate, logout]);

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
            setErrorString('');
            setErrors400({});

            const response = await axios({
                method: 'put',
                url: 'https://localhost:7195/api/user',
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            console.log('User edited:', response.data);

            window.location.reload();

        } catch (error) {
            if (error.response && error.response.status === 400 && typeof (error.response.data) !== "string") {
                if (error.response.data.errors !== undefined) {
                    setErrors400(error.response.data.errors);
                } else {
                    setErrors400(error.response.data);
                }
            } else if (typeof (error.response.data) === "string") {
                setErrorString(error.response.data);
                console.log(error.response.data);
            } else {
                console.error('Edit failed:', error.response.data);
            }
        }

    };

    const handleUserDeactivation = () => {
        logout();
        navigate('/login');
    };

    return (
        <>
            {userDetails &&
                <GridColumn stretched width={7}>
                    <Form onSubmit={handleSubmit} className='profile-details-container' error={!!errorString} >
                        <Form.Group widths='equal'>
                            <Form.Field
                                error={errors400.FirstName && { content: errors400.FirstName.join(' '), pointing: 'below' }}
                                control={Input}
                                label='Име'
                                value={userDetails.firstName}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    firstName: e.target.value
                                }))}
                            />
                            <Form.Field
                                error={errors400.LastName && { content: errors400.LastName.join(' '), pointing: 'below' }}
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
                                error={errors400.Username && { content: errors400.Username.join(' '), pointing: 'below' }}
                                control={Input}
                                label='Потребителско име'
                                value={userDetails.username}
                                onChange={(e) => setUserDetails(prevDetails => ({
                                    ...prevDetails,
                                    username: e.target.value
                                }))}
                            />
                            <Form.Field
                                error={errors400.Email && { content: errors400.Email.join(' '), pointing: 'below' }}
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
                                <Image src={imageUrl + userDetails.image} size='small' widths={2} className='profile-image' />
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
                                error={errors400.Password && { content: errors400.Password.join(' '), pointing: 'below' }}
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
                                error={errors400.NewPassword && { content: errors400.NewPassword.join(' '), pointing: 'below' }}
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
                                error={errors400.RepeatNewPassword && { content: errors400.RepeatNewPassword.join(' '), pointing: 'below' }}
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

                        {errorString && <Message error content={errorString} />}
                        <div className="sumbit button container">
                            {user.roles !== 'Admin'
                                ?
                                <DeactivateUserModal userDetails={userDetails} onDeactivate={handleUserDeactivation} />
                                :
                                <Button disabled className='delete'>Деактивиране на профила</Button>}
                            <Button type='submit' name='deactivate' className='save-btn'>Запазване</Button>
                        </div>
                    </Form>
                </GridColumn>
            }
        </>
    );
}