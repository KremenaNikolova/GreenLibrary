import { Card, CardHeader, CardGroup, CardMeta, CardDescription, Image, Grid, GridColumn, GridRow, Divider } from 'semantic-ui-react';

const imagesPath = '/assets/';
export default function ArticlesCards({ articles }) {
    return (
        <Grid devided='vertically'>
            <GridRow columns={2}>
                {articles.slice(0, 2).map(article => (
                    <GridColumn key={article.id}>
                        <CardGroup itemsPerRow={1}>
                            <Card className='card-image-container'>
                                <Image src={imagesPath + article.image} className='card-image' />
                                <CardHeader className="card-title">{article.title}</CardHeader>
                                <CardDescription>{article.user}</CardDescription>
                                <CardMeta>
                                    <span className='date'>{article.createdOn}</span>
                                </CardMeta>
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
                                <Image src={imagesPath + article.image} className='card-image' />
                                <CardHeader className="card-title">{article.title}</CardHeader>
                                <CardDescription>{article.user}</CardDescription>
                                <CardMeta>
                                    <span className='date'>{article.createdOn}</span>
                                </CardMeta>
                            </Card>
                        </CardGroup>
                    </GridColumn>
                ))}
            </GridRow>

        </Grid>
        

    )
}
