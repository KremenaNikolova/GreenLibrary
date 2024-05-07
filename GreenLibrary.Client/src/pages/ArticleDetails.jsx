import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Segment, Image, Icon, Button } from 'semantic-ui-react';
import { useParams, useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/AuthContext'
import './styles/articleDetails.css'

const imageUrl = 'https://localhost:7195/Images/'
export default function ArticleDetails() {
    const [article, setArticle] = useState(null);
    const [likesCount, setLikesCount] = useState(0);
    const [isToggle, setIsToggle] = useState(false);

    let { id } = useParams();
    const { user } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchArticle = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/articles/details?articleId=${id}`);
                setArticle(response.data);
                setLikesCount(response.data.likes);
            } catch (err) {
                console.log(err);
            }
        };

        fetchArticle();
    }, [id]);

    const handleLike = async () => {
        if (!user) {
            navigate('/login');
            return;
        }
        try {
            const response = await axios.post(`https://localhost:7195/api/articles/details/like?articleId=${id}`);

            setIsToggle(response.data);
            setLikesCount(currentLikes => response.data ? currentLikes - 1 : currentLikes + 1);
        } catch (err) {
            console.error("Like Article Error:", err);
        }

        
    };

    return (
        <>
            <Segment textAlign='right' className="likes-btn-container">
                <div className="h1-container">
                    {article && <h1>{article.title}</h1>}
                </div>
                <div>
                    {article && <Button onClick={handleLike} className="likes-btn" size='large'>
                        <Icon className="star-icon" name='star' />
                        {likesCount}</Button>}
                </div>
            </Segment>


            <Segment>
                {article && <Image src={imageUrl + article.image} size='medium' floated='left' />}
                {article && <p>
                    {article.description}
                </p>}
            </Segment>
        </>
    );
}