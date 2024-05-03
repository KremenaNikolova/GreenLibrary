import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input, Message } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import './styles/loginForm.css';

export default function LoginForm() {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errors400, setErrors400] = useState({});
    const [error401, setError401] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7195/api/user/login', { username, password });
            console.log('Logged in successfully');

            if (response.status === 200) {
                login({
                    username: response.data.username,
                    roles: response.data.roles
                });

                navigate('/');
            } else {
                console.error('Login failed:', response.data);
            }
        } catch (error) {
            if (error.response && error.response.status === 400) {
                setError401('');
                setErrors400(error.response.data.errors);
            } else if (error.response.status === 401) {
                setErrors400({});
                setError401(error.response.data);
            } else {
                console.error('Login failed:', error.response.data);
            }
        }
    };

    return (
        <>
            <Form onSubmit={handleSubmit} className='loginForm'
                error={!!error401}>
                <Form.Field
                    error={errors400.Username !== undefined}
                    control={Input}
                    label='Потребителско име*'
                    placeholder="example1992"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                {errors400.Username && <Message negative>{errors400.Username.join(' ')}</Message>}
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
                {error401 && <Message error content={error401} />}
                <div className='login-btn-container'>
                    <Button className='login-btn' type="submit">Влизане</Button>
                </div>
                <div className="links-container">
                    <Link className="register-link" to="/register">Регистрация</Link>
                    <Link className="forgotten-password" to="#">Забравена парола</Link>
                </div>
            </Form>
        </>
    );
}


