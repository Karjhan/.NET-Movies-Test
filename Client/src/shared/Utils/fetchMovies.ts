import { IMovie } from "../Models/movie";
import { IMovieFetchParams } from "../Models/movieFetchParams";
import { IPagination } from "../Models/pagination";

export default async function fetchMovies(params?: IMovieFetchParams): Promise<IPagination<Array<IMovie>>> {
    let basicURL: string = `${import.meta.env.VITE_BACKEND_API_URL}/Movies`;
    if (params?.actorIds || params?.genreId || params?.pageSize || params?.pageIndex || params?.search || params?.sort) {
        basicURL += "?";
        if (params.pageIndex && params.pageSize) {
            basicURL += `PageIndex=${params.pageIndex}&PageSize=${params.pageSize}`;
        }
        if (params.search) {
            basicURL += `&Search=${params.search}`;
        }
        if (params.sort) {
            basicURL += `&Sort=${params.sort}`;
        }
        if (params.genreId) {
            basicURL += `&GenreId=${params.genreId}`;
        }
        if (params.actorIds && params.actorIds.length > 0) {
            params.actorIds.forEach((actorId) => basicURL += `&ActorIds=${actorId}`);
        }
    } else {
        basicURL += '?PageIndex=1&PageSize=5'
    }
    const incomingJson = await fetch(basicURL, {
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
    });
    const moviesDataObject = await incomingJson.json();
    return moviesDataObject;
}