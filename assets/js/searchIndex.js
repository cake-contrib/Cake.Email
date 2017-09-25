
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"Attachment",
        content:"Attachment",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"EmailAliases",
        content:"EmailAliases",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"MailAddress",
        content:"MailAddress",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"EmailProvider",
        content:"EmailProvider",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"EmailResult",
        content:"EmailResult",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"EmailSettings",
        content:"EmailSettings",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"AttachmentBase",
        content:"AttachmentBase",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"LinkedResource",
        content:"LinkedResource",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/Attachment',
        title:"Attachment",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/EmailAliases',
        title:"EmailAliases",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/MailAddress',
        title:"MailAddress",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/EmailProvider',
        title:"EmailProvider",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/EmailResult',
        title:"EmailResult",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/EmailSettings',
        title:"EmailSettings",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/AttachmentBase',
        title:"AttachmentBase",
        description:""
    });

    y({
        url:'/Cake.Email/Cake.Email/api/Cake.Email/LinkedResource',
        title:"LinkedResource",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
