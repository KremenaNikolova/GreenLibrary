import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Segment, Image } from 'semantic-ui-react';
import { useParams } from 'react-router-dom';
import './styles/articleDetails.css'

const imageUrl = 'https://localhost:7195/Images/'
export default function ArticleDetails() {
    const [article, setArticle] = useState(null);
    let { id } = useParams();

    useEffect(() => {
        const fetchArticle = async () => {
            try {
                const response = await axios.get(`https://localhost:7195/api/articles/${id}`);
                setArticle(response.data);
                console.log(response.data);
            } catch (err) {
                console.log(err);
            }
        };

        fetchArticle();
    }, [id]);

    return (
        <Segment>
            {article && <Image src={imageUrl + article.image} size='medium' floated='left' />}
            {article && <p>
                {article.description}
            </p>}
        </Segment>
    );
}