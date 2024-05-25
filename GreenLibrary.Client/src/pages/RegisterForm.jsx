import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input, Message, Grid } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import './styles/registerForm.css';

export default function RegisterForm() {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [username, setUsername] = useState('');
    const [firstname, setFirstname] = useState('');
    const [lastname, setLastname] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [errors400, setErrors400] = useState({});
    const [errorString, seErrorString] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            seErrorString('');
            setErrors400({});

            const response = await axios.post('https://localhost:7195/api/user/register', { username, password, firstname, lastname, email, repeatPassword });
            console.log('Register in successfully');

            if (response.status === 200) {
                login({
                    username: response.data.username,
                    roles: response.data.roles
                });

                navigate('/');
            } else {
                console.error('Register failed:', response.data);
            }
        } catch (error) {
            if (error.response && error.response.status === 400 && typeof (error.response.data) !== "string") {
                setErrors400(error.response.data.errors);
            } else if (typeof (error.response.data) === "string") {
                seErrorString(error.response.data);
            } else {
                console.error('Register failed:', error.response.data);
            }
        }
    };

    return (
        <>
            <Grid className="back-btn-container">
                <Grid.Row>
                    <Grid.Column width={3}>
                        <Button className='cancelbutton' onClick={() => window.history.back()}>НАЗАД</Button>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
            <Form onSubmit={handleSubmit} className='registerForm'
                error={!!errorString}>

                <Form.Field
                    error={errors400.UserName !== undefined}
                    control={Input}
                    label='Потребителско име*'
                    placeholder="example1992"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                {errors400.UserName && <Message negative>{errors400.UserName.join(' ')}</Message>}

                <Form.Field
                    error={errors400.FirstName !== undefined}
                    control={Input}
                    label='Име*'
                    value={firstname}
                    onChange={(e) => setFirstname(e.target.value)}
                />
                {errors400.FirstName && <Message negative>{errors400.FirstName.join(' ')}</Message>}

                <Form.Field
                    error={errors400.LastName !== undefined}
                    control={Input}
                    label='Фамилия*'
                    value={lastname}
                    onChange={(e) => setLastname(e.target.value)}
                />
                {errors400.LastName && <Message negative>{errors400.LastName.join(' ')}</Message>}

                <Form.Field
                    error={errors400.Email !== undefined}
                    control={Input}
                    label='Имейл*'
                    placeholder="example@test.com"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                {errors400.Email && <Message negative>{errors400.Email.join(' ')}</Message>}

                <Form.Field
                    error={errors400.Password !== undefined}
                    control={Input}
                    label='Парола*'
                    placeholder="*********"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                {errors400.Password && <Message negative>{errors400.Password.join(' ')}</Message>}

                <Form.Field
                    error={errors400.RepeatPassword !== undefined}
                    control={Input}
                    label='Повторете паролата*'
                    placeholder="*********"
                    type="password"
                    value={repeatPassword}
                    onChange={(e) => setRepeatPassword(e.target.value)}
                />
                {errors400.RepeatPassword && <Message negative>{errors400.RepeatPassword.join(' ')}</Message>}

                {errorString && <Message error content={errorString} />}
                <div className='register-btn-container'>
                    <Button className='register-btn' type="submit">Регистрация</Button>\
                </div>
            </Form>
        </>
    );
}


