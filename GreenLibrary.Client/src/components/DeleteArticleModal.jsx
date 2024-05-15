import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';

export default function DeleteArticleModal({ articleId, onDeleteSuccess }) {
    const [open, setOpen] = useState(false);

    const handleDelete = async () => {
        try {
            await axios.delete(`https://localhost:7195/api/articles?articleId=${articleId}`);
            onDeleteSuccess(articleId);
            setOpen(false);
        } catch (error) {
            console.error('Failed to delete article:', error);
        }
    };

    return (
        <Modal
            open={open}
            trigger={<Button className='deactivate-btn' onClick={() => setOpen(true)}>Изтрий</Button>}
            onClose={() => setOpen(false)}
            size='small'
        >
            <Modal.Header>Изтриване на статия</Modal.Header>
            <Modal.Content>
                <p>Сигурни ли сте, че искате да изтриете тази статия?</p>
            </Modal.Content>
            <Modal.Actions>
                <Button color='black' onClick={() => setOpen(false)}>Отмени</Button>
                <Button className='deactivate-btn' onClick={handleDelete}>Изтрий</Button>
            </Modal.Actions>
        </Modal>
    );
}
