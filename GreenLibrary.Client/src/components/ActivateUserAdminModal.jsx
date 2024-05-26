import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios'

export default function ActivateUserAdminModal({ userDetails, onActivate }) {
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleDeactivate = async (e) => {
        e.preventDefault();

        try {
            const response = await axios({
                method: 'put',
                url: 'https://localhost:7195/api/admin/restore',
                params: { choosenUserId: userDetails.id },
                data: userDetails.isDeleted
            });
            console.log('User resotred:', response.data);
            onActivate(userDetails.id);
        } catch (error) {
            console.log(error.response);
        }

        handleClose();
    };

    return (
        <Modal
            trigger={<Button onClick={handleOpen} className='approved'>Възстановяване на профила</Button>}
            open={open}
            onClose={handleClose}
            size='small'
            header='Потвърждение'
            content='Сигурни ли сте, че искате да активирате този профил?'
            actions={[
                { key: 'cancel', content: 'Отказ', onClick: handleClose, color: "black" },
                { key: 'confirm', content: 'Активиране', onClick: handleDeactivate, color:"green"}
            ]}
        />
    );
}