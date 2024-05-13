import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/AuthContext'
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';

export default function DeactivateUserModal({ userDetails }) {
    const [open, setOpen] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleDeactivate = async (e) => {
        e.preventDefault();

        try {
            const response = await axios({
                method: 'put',
                url: 'https://localhost:7195/api/user/delete',
                data: userDetails.isDeleted
            });

            logout();
            navigate('/login');
            console.log('User deleted:', response.data);
        } catch (error) {
            console.log(error.response);
        }

        handleClose(); 
    };

    return (
        <Modal
            trigger={<Button onClick={handleOpen} className='deactivate-btn'>Деактивиране на профила</Button>}
            open={open}
            onClose={handleClose}
            size='small'
            header='Потвърждение'
            content='Сигурни ли сте, че искате да деактивирате вашия профил?'
            actions={[
                { key: 'cancel', content: 'Отказ', onClick: handleClose, color: "black" },
                { key: 'confirm', content: 'Деактивиране', onClick: handleDeactivate}
            ]}
        />
    );
}