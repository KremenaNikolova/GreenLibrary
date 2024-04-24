import { Fragment, useEffect, useState } from "react";
import axios from "../../../node_modules/axios/index";
import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import ArticlesCards from "../../components/ArticlesCards";

function App() {
  const [articles, setArticles] = useState([]);

  let sortedArticles = articles.sort((a, b) => a.createdOn > b.createdOn ? 1 : -1);

  useEffect(() => {
    axios.get("https://localhost:7195/api/articles").then((response) => {
      setArticles(response.data);
    });
  }, []);

  return (
    <Fragment>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
              <ArticlesCards articles={ sortedArticles } />
      </Container>
    </Fragment>
  );
}

export default App;
