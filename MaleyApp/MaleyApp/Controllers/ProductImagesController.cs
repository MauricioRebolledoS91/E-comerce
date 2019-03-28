using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MaleyApp.DAL;
using MaleyApp.Models;

namespace MaleyApp.Controllers
{
    public class ProductImagesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: ProductImages
        public ActionResult Index()
        {
            return View(db.ProductImages.ToList());
        }

        // GET: ProductImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImages/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            //// comprobar que el usuario ha introducido un archivo
            //if (file != null)
            //{
            //    //verifica que el archivo sea válido
            //    if (ValidateFile(file))
            //    {
            //        try
            //        {
            //            SaveFileToDisk(file);
            //        }
            //        catch (Exception)
            //        {
            //            ModelState.AddModelError("FileName", "Lo sentimos, se ha producido un error al guardar el archivo en el disco, inténtalo nuevamente ");
            //        }
            //    }
            //     else
            //     {
            //            ModelState.AddModelError("FileName", "El archivo debe ser tipo gif, png, jpeg o jpg y no debe tener un tamaño de más de 2 MB");
            //     }
            //}
            //    else
            //    {
            //        //si el usuario no a agregdo un archivo, le lanza el mensaje de error para que elija uno a ingresar
            //        ModelState.AddModelError("FileName", "Por favor elige un archivo");
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        db.ProductImages.Add(new ProductImage { FileName = file.FileName });
            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbUpdateException ex)
            //    {

            //        SqlException innerException = ex.InnerException.InnerException as SqlException;
            //        //este es el número de excepción de SQL para intentar insertar
            //        //una clave duplicada cuando se coloca un índice único en una tabla
            //        if (innerException != null && innerException.Number == 2601)
            //        {
            //            ModelState.AddModelError("FileName", "El archivo " + file.FileName +
            //            " ya existe en el sistema. Por favor, elimínelo y vuelva a intentarlo si desea volver a agregarlo.");

            //        }
            //        else
            //        {
            //            ModelState.AddModelError("FileName", "Lo sentimos, se ha producido un error al guardar en la base de datos. Inténtalo de nuevo.");
            //        }

            //             return View();

            //        }

            //        return RedirectToAction("Index");
            //    }

            bool allValid = true;
            string inValidFiles = "";
            db.Database.Log = sql => Trace.WriteLine(sql);
            //check the user has entered a file
            if (files[0] != null)
            {
                //if the user has entered less than ten files
                if (files.Length <= 10)
                {
                    //check they are all valid
                    foreach (var file in files)
                    {
                        if (!ValidateFile(file))
                        {
                            allValid = false;
                            inValidFiles += ", " + file.FileName;
                        }
                    }
                    //if they are all valid then try to save them to disk
                    if (allValid)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                SaveFileToDisk(file);
                            }
                            catch (Exception)
                            {
                                ModelState.AddModelError("FileName", "Sorry an error occurred saving the files to disk, please try again");
                            }
                        }
                    }
                        //else add an error listing out the invalid files
                        else
                        {
                            ModelState.AddModelError("FileName", "All files must be gif, png, jpeg or jpg and less than 2MB in size.The following files" + inValidFiles + " are not valid");
                        }
                    }
                //the user has entered more than 10 files
                else
                {
                    ModelState.AddModelError("FileName", "Please only upload up to ten files at a time");
                }
            }
            else
            {
                //if the user has not entered a file return an error message
                ModelState.AddModelError("FileName", "Please choose a file");
            }
            if (ModelState.IsValid)
            {
                bool duplicates = false;
                bool otherDbError = false;
                string duplicateFiles = "";

                foreach (var file in files)
                {
                    //try and save each file
                    var productToAdd = new ProductImage { FileName = file.FileName };
                    try
                    {
                        db.ProductImages.Add(productToAdd);
                        db.SaveChanges();
                    }
                    //if there is an exception check if it is caused by a duplicate file
                    catch (DbUpdateException ex)
                    {
                        SqlException innerException = ex.InnerException.InnerException as SqlException;
                        if (innerException != null && innerException.Number == 2601)
                        {
                            duplicateFiles += ", " + file.FileName;
                            duplicates = true;
                            //Para eliminar una entrada del DbContext, el estado de la entidad debe configurarse para desconectarse. Para separar el archivo actual
                            db.Entry(productToAdd).State = EntityState.Detached;
                        }
                        else
                        {
                            otherDbError = true;
                        }
                    }
                }

                //add a list of duplicate files to the error message
                if (duplicates)
                {
                    ModelState.AddModelError("FileName", "All files uploaded except the files" +
                    duplicateFiles + ", which already exist in the system." + " Please delete them and try again if you wish to re - add them");
                    return View();
                }
                else if (otherDbError)
                {
                        ModelState.AddModelError("FileName", "Sorry an error has occurred saving to the database, please try again");
                        return View();
                }
                 return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileName")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Este método debería ir en una clase aparte
        //pero por demostración, se dejará aquí mientras tanto
        private bool ValidateFile(HttpPostedFileBase file)
        {
            //Obteniendo extensión del archivo
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            //tipos de archivos permitidos
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            //comprueba si el archivo es del tipo de extensión permitido
            //También comprueba si está entre 0 bytes y 2MB de tamaño
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) &&
            allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        //guarda archivo de imagen en 
        private void SaveFileToDisk(HttpPostedFileBase file)
        {

            WebImage img = new WebImage(file.InputStream);
            //modifica el tamaño de las imágenes si es necesario
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constants.ProductImagePath + file.FileName);
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constants.ProductThumbnailPath + file.FileName);
        }
    }
}
