import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext'
import './styles/loginForm.css';

export default function LoginForm() {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7195/api/user/login', { username, password });
            console.log('Logged in successfully');

            if (response.status === 200) {
                login({ username });
                navigate('/');
            } else {
                console.error('Login failed:', response.data);
            }
        } catch (error) {
            console.error('Login failed:', error.response.data);
        }
    };

    return (
        <Form onSubmit={handleSubmit} className='loginForm'>
            <Form.Field
                control={Input}
                label='Потребителско име'
                placeholder="example1992"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
            />
            <Form.Field
                control={Input}
                label='Парола'
                placeholder="*********"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <div className='login-btn-container'>
                <Button className='login-btn' type="submit">Login</Button>
            </div>
        </Form>
    );
}


