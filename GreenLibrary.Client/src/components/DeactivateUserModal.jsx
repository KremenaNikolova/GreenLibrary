import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';

export default function DeactivateUserModal({ userDetails, onDeactivate }) {
    const [open, setOpen] = useState(false);

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

            console.log('User deleted:', response.data);
            onDeactivate();
        } catch (error) {
            console.log(error.response);
        }

        handleClose(); 
    };

    return (
        <Modal
            trigger={<Button onClick={handleOpen} className='delete'>Деактивиране на профила</Button>}
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