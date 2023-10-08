import { IPagination } from "./pagination";

export interface ICustomPaginationProps<T>{
    paginationData: IPagination<T>,
    setPagination: (arg0: IPagination<T>) => void;
    flag: boolean;
    setChangeFlag: (arg0: boolean) => void;
}