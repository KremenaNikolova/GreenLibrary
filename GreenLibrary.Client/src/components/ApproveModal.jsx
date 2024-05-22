import React, { useState } from 'react';
import { Button, Modal } from 'semantic-ui-react';
import axios from 'axios';
import './styles/approveModal.css';
export default function ApproveModal({ articleId, articles, setArticles }) {
    const [open, setOpen] = useState(false);

    const handleApprove = async (articleId) => {
        try {
            await axios.post(`https://localhost:7195/api/admin/articles?articleId=${articleId}`, null, {
                params: { articleId: articleId }
            });

            setArticles(articles.map(article => article.id === articleId
                ? { ...article, isApproved: true }
                : article));

        } catch (error) {
            console.log('Error Approve article:', error);
        }
    };
    return (
        <Modal
            open={open}
            trigger={<Button className="approved" onClick={() => setOpen(true)}>Одобри</Button>}
            onClose={() => setOpen(false)}
            size='small'
        >
            <Modal.Header>Одобряване</Modal.Header>
            <Modal.Content>
                <p>Сигурни ли сте, че искате да одобрите тази статия?</p>
            </Modal.Content>
            <Modal.Actions>
                <Button color='black' onClick={() => setOpen(false)}>Отмени</Button>
                <Button className="approved" onClick={() => handleApprove(articleId)}>Одобри</Button>
            </Modal.Actions>
        </Modal>
    );
}