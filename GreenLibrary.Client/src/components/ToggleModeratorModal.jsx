import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';

export default function ToggleModeratorModal({ userDetails, buttonMesasge, className, onToggle }) {
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleToggleModerator = async () => {
        try {
            await axios.put(`https://localhost:7195/api/admin/toggle-moderator`, null, {
                params: { choosenUserId: userDetails.id }
            });
            onToggle(userDetails.id);
            console.error('Статуса на потребителят беше успешно променен.');
        } catch (error) {
            console.error('Error sending new password:', error);
        }
        handleClose();
    };

    return (
        <Modal
            trigger={<Button onClick={handleOpen} className={className}>{buttonMesasge}</Button>}
            open={open}
            onClose={handleClose}
            size='small'
            header='Потвърждение'
            content='Сигурни ли сте, че искате да промените статуса на потребителя?'
            actions={[
                { key: 'cancel', content: 'Отказ', onClick: handleClose, color: "black" },
                { key: 'confirm', content: 'Потвърждавам', onClick: handleToggleModerator, color: "green" }
            ]}
        />
    );
}
