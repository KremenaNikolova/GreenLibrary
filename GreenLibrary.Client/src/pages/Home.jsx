import { useEffect, useState } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import ArticlesCards from "../components/ArticlesCards";
import { useAuth } from '../hooks/AuthContext';
import './styles/home.css'

function Home() {
    const { user, logout } = useAuth();
    const [articles, setArticles] = useState([]);

    //let sortedArticles = articles.sort((a, b) => a.createdOn < b.createdOn ? 1 : -1);

    useEffect(() => {
        if (!user) {
            window.onload = logout();
        }
        axios.get("https://localhost:7195/api/articles").then((response) => {
            setArticles(response.data);
        });
    }, [user, logout]);

    return (
        <Container className='home container'>
            <ArticlesCards articles={articles} />
        </Container>
    );
}

export default Home;