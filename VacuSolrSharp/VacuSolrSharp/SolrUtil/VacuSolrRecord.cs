using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using org.apache.solr.SolrSharp.Results;
using org.apache.solr.SolrSharp.Indexing;
using System.Collections;

namespace VacuSolrSharp
{
    public class VacuSolrRecord : SearchRecord
    {
        public VacuSolrRecord()
            : base() {
        }

        public VacuSolrRecord(XmlNode xnRecord)
            : base(xnRecord)
        {
        }

        [IndexField("id")]
        public string Id { get; set;}

        [IndexField("*")]
        public IDictionary<string, object> Fields { get; set; }

        public string RenderHtml(SolrSearchVO solrSearchVO) {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='result_doc'>")
                .Append("<div><span class='tit'>[자료ID]</span> " + (String.IsNullOrEmpty(this.Id) ? "" : this.Id) + "</div>");

            if (this.Fields != null) {
                foreach (KeyValuePair<string, object> kv in this.Fields) {
                    sb.Append("<div><span class='tit'>[" + MsgCnvUtil.toCnv(SolrUtil.coll_name, kv.Key) + "]</span> ");

                    if ((kv.Value as ICollection) != null) {
                        ICollection values = kv.Value as ICollection;
                        foreach (object val in values)
                            sb.Append(val.ToString() + " ");
                    } else
                        sb.Append(kv.Value.ToString());
                    sb.Append("</div>");
                }
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        public string RenderGroupHtml(SolrSearchVO solrSearchVO) {
            return RenderHtml(solrSearchVO);
        }
    }

    /*
        public class VacuSolrRecord : SearchRecord
        {
            public VacuSolrRecord(XmlNode xnRecord)
                : base(xnRecord)
            {
            }

            private string id = null;
            [IndexField("id")]
            public string Id
            {
                get { return this.id; }
                set { this.id = value; }
            }

            private string taskid = null;
            [IndexField("taskid")]
            public string Taskid
            {
                get { return this.taskid; }
                set { this.taskid = value; }
            }

            private string productid = null;
            [IndexField("productid")]
            public string Productid
            {
                get { return this.productid; }
                set { this.productid = value; }
            }

            private string section_name = null;
            [IndexField("section_name")]
            public string Section_name
            {
                get { return this.section_name; }
                set { this.section_name = value; }
            }

            private string section_area = null;
            [IndexField("section_area")]
            public string Section_area
            {
                get { return this.section_area; }
                set { this.section_area = value; }
            }

            private string section_type = null;
            [IndexField("section_type")]
            public string Section_type
            {
                get { return this.section_type; }
                set { this.section_type = value; }
            }

            private string maintitle = null;
            [IndexField("maintitle")]
            public string Maintitle
            {
                get { return this.maintitle; }
                set { this.maintitle = value; }
            }

            private string title_k = null;
            [IndexField("title_k")]
            public string Title_k
            {
                get { return this.title_k; }
                set { this.title_k = value; }
            }

            private string title_c = null;
            [IndexField("title_c")]
            public string Title_c
            {
                get { return this.title_c; }
                set { this.title_c = value; }
            }

            private string content = null;
            [IndexField("content")]
            public string Content
            {
                get { return this.content; }
                set { this.content = value; }
            }

            private string explanation = null;
            [IndexField("explanation")]
            public string Explanation
            {
                get { return this.explanation; }
                set { this.explanation = value; }
            }

            private string url = null;
            [IndexField("url")]
            public string Url
            {
                get { return this.url; }
                set { this.url = value; }
            }

            private string keywords = null;
            [IndexField("keywords")]
            public string Keywords
            {
                get { return this.keywords; }
                set { this.keywords = value; }
            }

            private string imagetype = null;
            [IndexField("imagetype")]
            public string Imagetype
            {
                get { return this.imagetype; }
                set { this.imagetype = value; }
            }

            private string mainimage = null;
            [IndexField("mainimage")]
            public string Mainimage
            {
                get { return this.mainimage; }
                set { this.mainimage = value; }
            }

            private List<string> extendinfo = new List<string>();
            [IndexField("extendinfo")]
            public string[] Extendinfo
            {
                get { return this.extendinfo.ToArray(); }
                set { this.extendinfo.AddRange(value); }
            }

            private string f_period = null;
            [IndexField("f_period")]
            public string F_period
            {
                get { return this.f_period; }
                set { this.f_period = value; }
            }

            private string f_region = null;
            [IndexField("f_region")]
            public string F_region
            {
                get { return this.f_region; }
                set { this.f_region = value; }
            }

            private string f_owner = null;
            [IndexField("f_owner")]
            public string F_owner
            {
                get { return this.f_owner; }
                set { this.f_owner = value; }
            }

            private string f_docstyle = null;
            [IndexField("f_docstyle")]
            public string F_docstyle
            {
                get { return this.f_docstyle; }
                set { this.f_docstyle = value; }
            }

            private float score = 0;
            [IndexField("score")]
            public float Score
            {
                get { return this.score; }
                set { this.score = value; }
            }

            public string RenderHtml(SolrSearchVO solrSearchVO)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<div class='result_doc'>")
                    .Append("<div><span class='tit'>[통합메타ID]</span> " + Id + "</div>")
                    .Append("<div><span class='tit'>[대표표제어]</span> " + Maintitle + "</div>")
                    .Append("<div><span class='tit'>[본문]</span> " + Content + "</div>")
                    .Append("<div><span class='tit'>[설명문]</span> " + Explanation + "</div>")
                    .Append("<div><span class='tit'>[바로가기]</span> <a href='" + Url + "' target='_blank'>" + Url + "</a></div>");

                sb.Append("<div><span class='tit'>[확장필드]</span> ");
                int cnt = 0;
                foreach (var c in Extendinfo)
                {
                    if (cnt > 0) sb.Append(", ");
                    sb.Append(c);
                    cnt++;
                }
                sb.Append("</div>");

                sb.Append("<div><span class='tit'>[Score]</span> " + Score.ToString() + "</div>");

                sb.Append("</div>");

                return sb.ToString();
            }
        }
    */
    /*
        public class VacuSolrRecord : SearchRecord
        {
            public VacuSolrRecord(XmlNode xnRecord)
                : base(xnRecord)
            {
            }

            private string id = null;
            [IndexField("id")]
            public string Id
            {
                get { return this.id; }
                set { this.id = value; }
            }

            private string sku = null;
            [IndexField("sku")]
            public string Sku
            {
                get { return this.sku; }
                set { this.sku = value; }
            }

            private string name = null;
            [IndexField("name")]
            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            private string manu = null;
            [IndexField("manu")]
            public string Manu
            {
                get { return this.manu; }
                set { this.manu = value; }
            }

            private List<string> cat = new List<string>();
            [IndexField("cat")]
            public string[] Cat
            {
                get { return this.cat.ToArray(); }
                set { this.cat.AddRange(value); }
            }

            private List<string> features = new List<string>();
            [IndexField("features")]
            public string[] Features
            {
                get { return this.features.ToArray(); }
                set { this.features.AddRange(value); }
            }

            private string includes = null;
            [IndexField("includes")]
            public string Includes
            {
                get { return this.includes; }
                set { this.includes = value; }
            }

            private float weight = float.MinValue;
            [IndexField("weight")]
            public float Weight
            {
                get { return this.weight; }
                set { this.weight = value; }
            }

            private float price = float.MinValue;
            [IndexField("price")]
            public float Price
            {
                get { return this.price; }
                set { this.price = value; }
            }

            private int popularity = int.MinValue;
            [IndexField("popularity")]
            public int Popularity
            {
                get { return this.popularity; }
                set { this.popularity = value; }
            }

            private bool instock = false;
            [IndexField("inStock")]
            public bool InStock
            {
                get { return this.instock; }
                set { this.instock = value; }
            }

            private DateTime timestamp = DateTime.MinValue;
            [IndexField("timestamp")]
            public DateTime TimeStamp
            {
                get { return this.timestamp; }
                set { this.timestamp = value; }
            }

            public string RenderHtml(SolrSearchVO solrSearchVO)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<div class='result_doc'>")
                    .Append("<div class='tit'>" + Name + "</div>")
                    .Append("ID: " + Id + "<br />")
                    .Append("Manu: <a href='?" + solrSearchVO.UrlSetFacet("manu_exact", Manu) + "'>" + Manu + "</a><br />")
                    .Append("Price: <span class='price'>" + ((float.MinValue==Price)?"":Price.ToString("C")) + "</span><br />");

                int cnt = 0;
                sb.Append("Categories: ");
                foreach (var c in Cat)
                {
                    if (cnt > 0) sb.Append(", ");
                    sb.Append("<a href='?" + solrSearchVO.UrlSetFacet("cat", c) + "'>" + c + "</a>");
                    cnt++;
                }
                sb.Append("<br />");

                sb.Append("features:");

                cnt = 0;
                foreach (var c in Features)
                {
                    if (cnt > 0) sb.Append(", ");
                    sb.Append(c);
                    cnt++;
                }
                sb.Append("<br />")

                .Append("Includes: " + Includes + "<br />")
                .Append("weight: " + ((float.MinValue == Weight) ? "" : Weight.ToString()) + "<br />")
                .Append("instock: " + InStock + "<br />")
                .Append("Popularity: " + Popularity)
                .Append("</div>");


                return sb.ToString();
            }
        }
    */
}
