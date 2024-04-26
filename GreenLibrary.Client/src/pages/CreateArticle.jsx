import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Form, Input, Select, TextArea, List, Segment } from 'semantic-ui-react';

function ArticleForm() {
    const [title, setTitle] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');
    const [imagePath, setImage] = useState('');
    const [tags, setTags] = useState([]);
    const [categories, setCategories] = useState([]);
    const [tagInput, setTagInput] = useState('');

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get('https://localhost:7195/api/categories');
                const categoryOptions = response.data.map(c => ({
                    key: c.id,
                    value: c.id,
                    text: c.name
                }));
                setCategories(categoryOptions);
            } catch (error) {
                console.log('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const articleData = {
            title,
            categoryId,
            description,
            imagePath,
            tags
        };

        try {
            console.log('Sending:', articleData);
            const response = await axios.post('https://localhost:7195/api/articles/create', articleData, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            console.log('Article created:', response.data);

            setTitle('');
            setCategoryId('');
            setDescription('');
            setImage('');
            setTags([]);
        } catch (error) {
            console.error('Failed to create article:', error);
        }
    };

    const handleAddTag = () => {
        if (tagInput) {
            const newTags = [...tags, tagInput];
            setTags(newTags);
            setTagInput('');
        }
    };

    return (
        <Form onSubmit={handleSubmit}>
            <Form.Field
                control={Input}
                label='Title'
                placeholder='Title'
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <Form.Field
                control={Select}
                label='Category'
                options={categories}
                placeholder='Select Category'
                value={categoryId}
                onChange={(e, { value }) => setCategoryId(value)}
                required
            />
            <Form.Field
                control={TextArea}
                label='Description'
                placeholder='Description'
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
            />
            <Form.Field
                control={Input}
                label='Image URL'
                placeholder='Image URL'
                value={imagePath}
                onChange={(e) => setImage(e.target.value)}
            />
            <Form.Field
                control={Input}
                action={{
                    content: 'Add Tag',
                    type: 'button',
                    onClick: handleAddTag
                }}
                value={tagInput}
                onChange={(e) => setTagInput(e.target.value)}
                placeholder='Add a tag'
            />
            <Segment>
                <List>
                    {tags.map((tag, index) => (
                        <List.Item key={index}>
                            {tag}
                            <Button size='tiny' negative onClick={() => setTags(tags.filter(t => t !== tag))}>Remove</Button>
                        </List.Item>
                    ))}
                </List>
            </Segment>
            <Form.Field control={Button} primary type='submit'>Submit</Form.Field>
        </Form>
    );
}

export default ArticleForm;
