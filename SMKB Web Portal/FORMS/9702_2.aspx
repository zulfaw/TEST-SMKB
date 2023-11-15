<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.Master" CodeBehind="9702_2.aspx.vb" Inherits="SMKB_Web_Portal._9702" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

            <!-- FIRST TAB  -->
            <div id="970201" class="tabcontent" style="display:none">
                <span onclick="this.parentElement.style.display='none'" class="topright">&times</span>
                    <div class="table-title">
                        <h6>Senarai Staf</h6>
                    </div>


                    <div class="search-filter">
                        <div class="row justify-content-center">
                            <div class="col-md-4">
                                <div class="form-group col-md-12 row">
                                    <label class="col-sm-4 col-form-label">Jabatan</label>
                                    <div class="col-sm-8">
                                        <select class="form-control searchable-dropdown">
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option>4</option>
                                            <option>5</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group col-md-12 row">
                                    <label class="col-sm-4 col-form-label">Status</label>
                                    <div class="col-sm-8">
                                        <select class="form-control searchable-dropdown">
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option>4</option>
                                            <option>5</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="btn btn-outline"><i class="fa fa-search"
                                        style="padding-right: 10px;"></i>Cari
                                </div>
                            </div>
                        </div>

                        <div class="form-row justify-content-center">



                        </div>

                    </div>

                    <div class="filter-table-function">
                        <div class="show-record">
                            <p>Tunjukkan</p>
                            <select class="form-control">
                                <option>5</option>
                                <option>10</option>
                                <option>20</option>
                                <option>50</option>
                            </select>
                            <p>Rekod</p>
                        </div>
                        <div class="search-form">
                            <input class="form-control" type="text" placeholder="Search">
                        </div>
                    </div>
                    <div class="table-list table-responsive">
                        <table class="table table-striped table-bordered ">
                            <thead>
                                <tr>
                                    <th scope="col">Bil</th>
                                    <th scope="col">No Staf</th>
                                    <th scope="col">Nama Staf</th>
                                    <th scope="col">Jawatan</th>
                                    <th scope="col">Tahap Pengguna</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Tindakan</th>
                                </tr>
                            </thead>
                            <tbody>

                                <tr>
                                    <td>01</td>
                                    <td>03245</td>
                                    <td>Ali Bin Abu</td>
                                    <td>Admin</td>
                                    <td>System Admin</td>
                                    <td>Aktif</td>
                                    <td class="tindakan">
                                        <i class="fa fa-pencil-square-o edit" data-toggle="modal"
                                            data-target="#ModulForm"></i>
                                    </td>
                                </tr>
                                <!-- <tr>
                                    <td colspan="7" class="data-table-empty">No Record Found</td>
                                </tr> -->

                            </tbody>
                        </table>
                    </div>
                    <div class="table-navigation">
                        <div class="table-record">
                            <p>Jumlah Rekod:25</p>
                        </div>
                        <div class="table-page-nav">
                            <nav aria-label="...">
                                <ul class="pagination">
                                    <li class="page-item disabled">
                                        <span class="page-link">Previous</span>
                                    </li>
                                    <li class="page-item"><a class="page-link" href="#">1</a></li>
                                    <li class="page-item active">
                                        <span class="page-link">
                                            2
                                            <span class="sr-only">(current)</span>
                                        </span>
                                    </li>
                                    <li class="page-item"><a class="page-link" href="#">3</a></li>
                                    <li class="page-item">
                                        <a class="page-link" href="#">Next</a>
                                    </li>
                                </ul>
                            </nav>
                        </div>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="ModulForm" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalCenterTitle">Kemas kini peranan pengguna
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>

                                <div class="modal-body">
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="kodModul">Nama Staf</label>
                                            <input type="text" class="form-control" id="kod" placeholder="Ali Bin Abu"
                                                readonly>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label for="kodModul">No. Staf</label>
                                            <input type="text" class="form-control" id="kod" placeholder="02346"
                                                readonly>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="kodModul">Tahap Lama</label>
                                        <input type="text" class="form-control" id="kod" placeholder="Ali Bin Abu"
                                            readonly>
                                    </div>
                                    <div class="form-group">
                                        <label for="kodModul">Tahap Baharu</label>
                                        <select class="form-control searchable-dropdown">
                                            <option>System Admin</option>
                                            <option>2</option>
                                        </select>
                                    </div>

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <button type="button" class="btn btn-primary">Save changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            <div id="970202" class="tabcontent">
              <span onclick="this.parentElement.style.display='none'" class="topright">&times</span>
              <h3>...</h3>
              <p>|||</p> 
            </div>

            <div id="970203" class="tabcontent">
                <h3>...</h3>
                <p>|||</p>
            </div>

    <script>
        function openTab(evt, subMenu) {
            // Declare all variables
            var i, tabcontent, tablinks;

            //alert(subMenu);

            // Get all elements with class="tabcontent" and hide them
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            // Get all elements with class="tablinks" and remove the class "active"
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");

            }

            // Show the current tab, and add an "active" class to the button that opened the tab
            document.getElementById(subMenu).style.display = "block";
            evt.currentTarget.className += " active";
            //evt.currentTarget.className 

            //document.getElementById("defaultOpen").click();
        }



    </script>      

    

</asp:Content>
