export interface IMovie{
    id: string,
    name: string,
    description: string,
    year: number,
    rating: string,
    genres: Array<string>,
    actors: Array<string>,
    coverURL: string,
    imdbURL: string
}