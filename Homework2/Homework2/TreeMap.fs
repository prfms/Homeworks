module TreeMap

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Tip
let rec mapForTree f tree =
    match tree with
    | Tip -> Tip
    | Node(x, l, r) -> Node(f x, mapForTree f l, mapForTree f r)
