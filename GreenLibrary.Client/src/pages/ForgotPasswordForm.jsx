import React, { useState } from 'react';
import { Button, Form, Input, Message, Grid } from 'semantic-ui-react';
import axios from 'axios';

export default function ForgotPasswordForm() {
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7195/api/email/forgot-password', { email });
            setMessage(response.data);
        } catch (error) {
            if (error.response && error.response.status === 400) {
                setMessage(error.response.data);
            } else {
                setMessage('Възникна грешка при изпращането на нова парола.');
            }
        }
    };

    return (
        <>
            <Grid className="profile-container">
                <Grid.Row>
                    <Grid.Column width={3}>
                        <Button className='cancelbutton' onClick={() => window.history.back()}>НАЗАД</Button>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
            <Form onSubmit={handleSubmit} className='registerForm'
                error={!!message}>

                <Form.Field
                    control={Input}
                    label='Имейл*'
                    placeholder="example1992@test.com"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />

                {message && <Message error content={message} />}
                <div className='register-btn-container'>
                    <Button className='register-btn' type="submit">Изпрати нова парола</Button>
                </div>
            </Form>
        </>
    );
}