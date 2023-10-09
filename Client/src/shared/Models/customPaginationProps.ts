import { IMovieFetchParams } from "./movieFetchParams";
import { IPagination } from "./pagination";

export interface ICustomPaginationProps<T>{
    paginationParams: IMovieFetchParams,
    setPagination: (arg0: IMovieFetchParams) => void;
    paginationData: IPagination<T>
}