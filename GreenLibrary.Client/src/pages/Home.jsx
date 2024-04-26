import { useEffect, useState } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import ArticlesCards from "../elements/ArticlesCards"
function Home() {
    const [articles, setArticles] = useState([]);

    let sortedArticles = articles.sort((a, b) => a.createdOn > b.createdOn ? 1 : -1);

    useEffect(() => {
        axios.get("https://localhost:7195/api/articles").then((response) => {
            setArticles(response.data);
        });
    }, []);
  return (
      <Container style={{ marginTop: "7em" }}>
          <ArticlesCards articles={sortedArticles} />
      </Container>
  );
}

export default Home;