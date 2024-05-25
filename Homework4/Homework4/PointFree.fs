module Homework4.PointFree

let func x l = List.map (fun y -> y * x) l

let func'1 (x: int) : int list -> int list = List.map (fun (y: int) -> y * x)

let func'2 (x: int) : int list -> int list = List.map (fun (y: int) -> (*) x y)

let func'3 (x: int) : int list -> int list = List.map ((*) x)

let func'4 : int -> int list -> int list = List.map << (*)

