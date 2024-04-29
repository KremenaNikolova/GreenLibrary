import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input, Select, TextArea, List, Segment } from 'semantic-ui-react';
import './styles/createArticle.css'

function ArticleForm() {
    const navigate = useNavigate();
    const [title, setTitle] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');
    const [tags, setTags] = useState([]);
    const [categories, setCategories] = useState([]);
    const [tagInput, setTagInput] = useState('');
    const [imageFile, setImageFile] = useState(null);

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
            console.log('Article created:', response.data);

            setTitle('');
            setCategoryId('');
            setDescription('');
            setTags([]);
            setImageFile(null);

            navigate('/');
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
        <Form onSubmit={handleSubmit} className='createform'>
            <Form.Field
                control={Input}
                label='Заглавие'
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <Form.Field
                control={Select}
                label='Категория'
                options={categories}
                placeholder='Изберете категория'
                value={categoryId}
                onChange={(e, { value }) => setCategoryId(value)}
                required
            />
            <Form.Field
                control={TextArea}
                label='Описание'
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
            />
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
                onChange={(e) => setTagInput(e.target.value)}
                placeholder='Добавете тагове за търсене'
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
            <div className="sumbit button container">
                <Button className='submitbutton' type='submit'>Създай</Button>
            </div>
            
        </Form>
    );
}

export default ArticleForm;
