import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input, Select, TextArea, List, Message, Grid } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';
import './styles/createArticleForm.css';

export default function CreateArticleForm() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const [title, setTitle] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');
    const [tags, setTags] = useState([]);
    const [categories, setCategories] = useState([]);
    const [tagInput, setTagInput] = useState('');
    const [imageFile, setImageFile] = useState(null);
    const [errors400, setErrors400] = useState({});

    useEffect(() => {
        if (!user) {
            navigate('/login');
        }

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
    }, [navigate, user]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('title', title);
        formData.append('categoryId', categoryId);
        formData.append('description', description);
        tags.forEach(tag => formData.append('tags', tag));
        if (imageFile) {
            formData.append('imageFile', imageFile);
        }

        try {
            const response = await axios({
                method: 'post',
                url: 'https://localhost:7195/api/articles/create',
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            console.log('Article created', response.data);

            setTitle('');
            setCategoryId('');
            setDescription('');
            setTags([]);
            setImageFile(null);

            navigate('/');
        } catch (error) {
            setErrors400({});
            if (error.response && error.response.status === 400) {
                setErrors400(error.response.data.errors);
            } else {
                console.error('Login failed:', error.response.data);
                console.error('Failed to create article:', error);
            }
        }

    };

    const handleAddTag = () => {
        if (tagInput) {
            const newTags = [...tags, tagInput];
            setTags(newTags);
            setTagInput('');
        }
    };

    const handleBackClick = () => {
        navigate(-1);
    };

    return (
        <>
            <Form onSubmit={handleSubmit} className='createform' error={errors400.Username !== undefined}>
                <Form.Field
                    control={Input}
                    label='Заглавие*'
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                />
                {errors400.Title && <Message negative>{errors400.Title.join(' ')}</Message>}
                <Form.Field
                    control={Select}
                    label='Категория*'
                    options={categories}
                    placeholder='Изберете категория'
                    value={categoryId}
                    onChange={(e, { value }) => setCategoryId(value)}
                />
                <Form.Field
                    control={TextArea}
                    label='Съдържание*'
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
                {errors400.Description && <Message negative>{errors400.Description.join(' ')}</Message>}
                <Form.Field>
                    <label>Снимка</label>
                    <Input
                        type="file"
                        accept="image/*"
                        onChange={(e) => setImageFile(e.target.files[0])}
                    />
                </Form.Field>
                <Form.Field
                    control={Input}
                    action={{
                        content: 'Добави таг',
                        type: 'button',
                        onClick: handleAddTag
                    }}
                    value={tagInput}
                    placeholder='Добавете тагове за търсене'

                    onChange={(e) => setTagInput(e.target.value)}
                    onKeyDown={(e) => {
                        if (e.key === 'Enter') {
                            e.preventDefault();
                            handleAddTag();
                        }
                    }}
                />
                <List className="tags-container">
                    {tags.map((tag, index) => (
                        <List.Item key={index}>
                            <List.Item className="tag-container">{tag}
                                <button className="remove-tag-button" type='button' onClick={() => setTags(tags.filter(t => t !== tag))}> &times;</button>
                            </List.Item>
                        </List.Item>
                    ))}
                </List>
                <div className="sumbit button container">
                    <Button className='cancelbutton' type='button' onClick={handleBackClick}>НАЗАД</Button>
                    <Button className='submitbutton' type='submit'>Създай</Button>
                </div>
            </Form>
        </>
    );
}
