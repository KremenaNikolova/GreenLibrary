import { Card, CardHeader, CardGroup, CardMeta, CardDescription, Image, Grid, GridColumn, GridRow, Divider } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/AuthContext'
import './styles/articleCards.css'

const imageUrl = 'https://localhost:7195/Images/';
export default function ArticlesCards({ articles }) {
    const { user } = useAuth();

    return (
        <Grid devided='vertically'>
            <GridRow columns={2}>
                {articles.slice(0, 2).map(article => (
                    <GridColumn key={article.id}>
                        <CardGroup itemsPerRow={1}>
                            <Card className='card-image-container'>
                                <Link to={`/articles/${article.id}`}>
                                    <Image src={imageUrl + article.image} className='card-image' />
                                    <CardHeader className="card-title">{article.title}</CardHeader>
                                </Link>
                                <Link to={user ? `/user/${article.userId}` : `/login`}>
                                    <CardDescription>
                                        {article.user}
                                    </CardDescription>
                                </Link>
                                <CardDescription className='date'>
                                    Създадена на: {article.createdOn}
                                </CardDescription>
                            </Card>
                        </CardGroup>

                    </GridColumn>
                ))}
            </GridRow>

            <Divider section />

            <GridRow columns={3}>
                {articles.slice(2, 5).map(article => (
                    <GridColumn key={article.id}>
                        <CardGroup itemsPerRow={1}>
                            <Card className='card-image-container'>
                                <Link to={`/articles/${article.id}`}>
                                    <Image src={imageUrl + article.image} className='card-image' />
                                    <CardHeader className="card-title">{article.title}</CardHeader>
                                </Link>
                                <Link to={`/user/${article.userId}`}>
                                    <CardDescription>
                                        {article.user}
                                    </CardDescription>
                                </Link>
                                <CardDescription className='date'>
                                    Създадена на: {article.createdOn}
                                </CardDescription>
                            </Card>
                        </CardGroup>
                    </GridColumn>
                ))}
            </GridRow>

        </Grid>


    )
}
