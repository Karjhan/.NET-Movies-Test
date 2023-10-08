export interface IMovieFetchParams{
    pageIndex?: number,
    pageSize?: number,
    search?: string,
    sort?: string,
    actorIds?: Array<string>
    genreIds?: Array<string>
}