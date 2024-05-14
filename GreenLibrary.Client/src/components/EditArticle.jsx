import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Input, Select, TextArea, List, Message } from 'semantic-ui-react';
import { useAuth } from '../hooks/AuthContext';

export default function EditArticle({ articleId, onCancel }) {
    const navigate = useNavigate();
    const { user, logout } = useAuth();
    const [title, setTitle] = useState('');
    const [category, setCategory] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');
    const [tags, setTags] = useState([]);
    const [categories, setCategories] = useState([]);
    const [tagInput, setTagInput] = useState('');
    const [imageFile, setImageFile] = useState(null);
    const [errors400, setErrors400] = useState({});

    useEffect(() => {

        if (!user) { 
            logout();
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
    }, [navigate, user, logout]);

    useEffect(() => {
        const fetchArticleData = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/articles/details?articleId=${articleId}`);
                const article = response.data;
                setTitle(article.title);
                setCategory(article.category);
                setDescription(article.description);
                setTags(article.tags);
                if (article.imageFile) {
                    setImageFile(article.imageFile);
                }

                const foundCategory = categories.find(c => c.text === article.category);
                if (foundCategory) {
                    setCategoryId(foundCategory.value);
                }

                
            } catch (error) {
                console.log('Error fetching article data:', error);
            }
        };

        fetchArticleData();
    }, [navigate, user, logout, articleId, categories]);



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
                method: 'put',
                url: `https://localhost:7195/api/articles/edit?articleId=${articleId}`,
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            console.log('Article edited:', response.data);
            onCancel();
        } catch (error) {
            setErrors400({});
            if (error.response && error.response.status === 400) {
                setErrors400(error.response.data.errors);
            } else {
                console.error('Edit failed:', error.response.data);
                console.error('Failed to edit article:', error);
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

    const handleCategoryChange = (e, { value }) => {
        const selectedCategory = categories.find(c => c.value === value);
        setCategory(selectedCategory.text);
        setCategoryId(selectedCategory.value);
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
                    placeholder={category}
                    value={categoryId}
                    onChange={handleCategoryChange}
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
                <List>
                    {tags.map((tag, index) => (
                        <List.Item key={index}>
                            <div className="tag-container">{tag}
                                <Button size='tiny' negative type='button' onClick={() => setTags(tags.filter(t => t !== tag))}>Премахване</Button>
                            </div>
                        </List.Item>
                    ))}
                </List>
                <div className="sumbit button container">
                    <Button className='submitbutton' type='submit'>Запази</Button>
                    <Button className='cancelbutton' type='button' onClick={onCancel}>Отмени</Button>

                </div>

            </Form>
        </>
    );
}