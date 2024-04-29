import { useEffect, useState } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import ArticlesCards from "../elements/ArticlesCards"
import './styles/home.css'
function Home() {
    const [articles, setArticles] = useState([]);

    let sortedArticles = articles.sort((a, b) => a.createdOn < b.createdOn ? 1 : -1);

    useEffect(() => {
        axios.get("https://localhost:7195/api/articles").then((response) => {
            setArticles(response.data);
        });
    }, []);
  return (
      <Container className='home container'>
          <ArticlesCards articles={sortedArticles} />
      </Container>
  );
}

export default Home;