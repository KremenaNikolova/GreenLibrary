import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';

export default function SendNewPasswordModal({ userDetails }) {
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSendPassword = async () => {
        try {
            await axios.post(`https://localhost:7195/api/email/forgot-password`, {
                email: userDetails.email
            });
            console.error('Паролата беше успешно изпратена.');
        } catch (error) {
            console.error('Error sending new password:', error);
        }
        handleClose();
    };

    return (
        <Modal
            trigger={<Button onClick={handleOpen} className='approved'>Изпращане на нова парола</Button>}
            open={open}
            onClose={handleClose}
            size='small'
            header='Потвърждение'
            conten='Сигурни ли сте, че искате да изпратите нова парола на потребителя?'
            actions={[
                { key: 'cancel', content: 'Отказ', onClick: handleClose, color: "black" },
                { key: 'confirm', content: 'Изпращане', onClick: handleSendPassword, color: "green" }
            ]}
        />
    );
}
