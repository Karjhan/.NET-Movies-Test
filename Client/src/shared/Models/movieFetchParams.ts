export interface IMovieFetchParams{
    pageIndex?: number,
    pageSize?: number,
    search?: string,
    sort?: string,
    actorIds?: Array<string>
    genreId?: string
}