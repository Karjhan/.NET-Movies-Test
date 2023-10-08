import { IMovie } from "../Models/movie";
import { IMovieFetchParams } from "../Models/movieFetchParams";
import { IPagination } from "../Models/pagination";

export default async function fetchMovies(params?: IMovieFetchParams): Promise<IPagination<Array<IMovie>>> {
    let basicURL: string = `${import.meta.env.VITE_BACKEND_API_URL}/Movies`;
    if (params?.actorIds || params?.genreIds || params?.pageSize || params?.pageIndex || params?.search || params?.sort) {
        basicURL += "?";
        if (params.pageIndex && params.pageSize) {
            basicURL += `PageIndex=${params.pageIndex}&PageSize=${params.pageSize}`
        } else {
            basicURL += 'PageIndex=1&PageSize=5'
        }
    }
    const incomingJson = await fetch(`${import.meta.env.VITE_BACKEND_API_URL}/Movies?PageIndex=${params === undefined ? 1 : params.pageIndex}&PageSize=${params === undefined ? 5 : params.pageSize}`, {
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
    });
    const moviesDataObject = await incomingJson.json();
    return moviesDataObject;
}